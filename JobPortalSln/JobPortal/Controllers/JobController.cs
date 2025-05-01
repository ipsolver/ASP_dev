using JobPortal.Infrastructure;
using JobPortal.Models;
using JobPortal.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using JobPortal.Repositories;

namespace JobPortal.Controllers
{
    public class JobController : Controller
    {
        private IJobPortalRepository repository;
        private readonly IApplicationRepository _applicationRepository;
        public int PageSize = 5;

        public JobController(IJobPortalRepository repo, IApplicationRepository applicationRepo)
        {
            repository = repo;
            _applicationRepository = applicationRepo;
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
