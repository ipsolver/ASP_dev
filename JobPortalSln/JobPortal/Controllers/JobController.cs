using JobPortal.Infrastructure;
using JobPortal.Models;
using JobPortal.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using JobPortal.Repositories;
using Microsoft.AspNetCore.SignalR;
using JobPortal.Hubs;


namespace JobPortal.Controllers
{
    public class JobController : Controller
    {
        private IJobPortalRepository repository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IHubContext<NotificationHub> _hubContext;
        public int PageSize = 5;

        public JobController(IJobPortalRepository repo, IApplicationRepository applicationRepo, IHubContext<NotificationHub> hubContext)
        {
            repository = repo;
            _applicationRepository = applicationRepo;
            _hubContext = hubContext;
        }

        public IActionResult Index(string category, int page = 1)
        {
            var jobs = repository.Jobs
                .Where(j => category == null || category == "Всі" || j.Category.CategoryName == category)
                .OrderBy(j => j.JobID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);

            var viewModel = new JobsList
            {
                Jobs = jobs,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Jobs.Count(j => category == null || category == "Всі" || j.Category.CategoryName == category)
                },
                CurrentCategory = category
            };

            return View(viewModel);
        }


        // GET: Job/Create
        public IActionResult Create()
        {
            ViewBag.Categories = repository.Categories.ToList();
            return View();
        }

        // POST: Job/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Job job)
        {
            if (ModelState.IsValid)
            {
                repository.CreateJob(job);
                repository.SaveChanges();

                var categoryName = repository.Categories
                    .FirstOrDefault(c => c.CategoryID == job.CategoryID)?.CategoryName ?? "Категорія";

                var subscribedUsers = repository.UserCategories
                    .Where(uc => uc.CategoryID == job.CategoryID)
                    .Select(uc => uc.UsersID)
                    .Distinct()
                    .ToList();

                foreach (var userId in subscribedUsers)
                {
                    var notification = new Notification
                    {
                        UsersID = userId,
                        Message = $"Нова вакансія у категорії: {categoryName}",
                        CreatedAt = DateTime.Now,
                        IsRead = false
                    };
                    repository.AddNotification(notification);
                }

                repository.SaveChanges();

                var notificationMessage = $"Нова вакансія у категорії: {categoryName}";

                var tasks = subscribedUsers.Select(userId =>
                    _hubContext.Clients.User(userId.ToString())
                    .SendAsync("ReceiveNotification", notificationMessage)
                );
                await Task.WhenAll(tasks);

                return RedirectToAction("Index");
            }

            ViewBag.Categories = repository.Categories.ToList();
            return View(job);
        }



        public IActionResult Apply(int jobId)
        {
            string sessionKey = "application_" + jobId;

            var application = HttpContext.Session.GetJson<Application>(sessionKey) ?? new Application { JobID = jobId };

            return View(application);
        }


        [HttpPost]
        public IActionResult SaveApplicationToSession([FromBody] ApplicationData applicationData)
        {
            string sessionKey = "application_" + applicationData.JobId;

            HttpContext.Session.SetJson(sessionKey, applicationData.Application);

            return Ok();
        }

        public class ApplicationData
        {
            public int JobId { get; set; }
            public Application Application { get; set; }
        }


    }
}
