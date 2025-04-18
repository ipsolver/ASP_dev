using Microsoft.AspNetCore.Mvc;
using JobPortal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace JobPortal.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly IJobPortalRepository repository;

        public ApplicationsController(IJobPortalRepository repo)
        {
            repository = repo;
        }

        public IActionResult Index()
        {
            var apps = repository.Applications
                .Select(a => new Application
                {
                    ApplicationID = a.ApplicationID,
                    Job = a.Job,
                    Candidate = a.Candidate,
                    CoverLetter = a.CoverLetter,
                    Resume = a.Resume,
                    ApplicationDate = a.ApplicationDate,
                    Status = a.Status
                }).ToList();

            return View(apps);
        }

        public IActionResult Details(int id)
        {
            var app = repository.Applications
                .Include(a => a.Job)
                .Include(a => a.Candidate)
                .FirstOrDefault(a => a.ApplicationID == id);
            if (app == null) return NotFound();
            return View(app);
        }

        public IActionResult Create()
        {
            ViewBag.Jobs = new SelectList(repository.Jobs, "JobID", "Title");
            ViewBag.Users = new SelectList(repository.Users, "UsersID", "Login");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Application app)
        {
            Console.WriteLine($"JobID: {app.JobID}");
            Console.WriteLine($"CandidateID: {app.CandidateID}");
            Console.WriteLine($"Status: {app.Status}");
            if (ModelState.IsValid)
            {
                app.Status = "Pending";
                repository.CreateApplication(app);
                return RedirectToAction(nameof(Index));
            }
            foreach (var kvp in ModelState)
            {
                Console.WriteLine($"{kvp.Key}: {(kvp.Value.RawValue ?? "null")}  | Errors: {string.Join(", ", kvp.Value.Errors.Select(e => e.ErrorMessage))}");
            }

            foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine("Validation error: " + modelError.ErrorMessage);
            }

            ViewBag.Jobs = new SelectList(repository.Jobs, "JobID", "Title", app.JobID);
            ViewBag.Users = new SelectList(repository.Users, "UsersID", "Login", app.CandidateID);
            return View(app);
        }

        public IActionResult Edit(int id)
        {
            var app = repository.Applications.FirstOrDefault(a => a.ApplicationID == id);
            if (app == null) return NotFound();

            ViewBag.Jobs = new SelectList(repository.Jobs, "JobID", "Title", app.JobID);
            ViewBag.Users = new SelectList(repository.Users, "UsersID", "Login", app.CandidateID);
            return View(app);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Application app)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateApplication(app);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Jobs = new SelectList(repository.Jobs, "JobID", "Title", app.JobID);
            ViewBag.Users = new SelectList(repository.Users, "UsersID", "Login", app.CandidateID);
            return View(app);
        }

        public IActionResult Delete(int id)
        {
            var app = repository.Applications
                    .Include(a => a.Job)
                    .Include(a => a.Candidate)
                    .FirstOrDefault(a => a.ApplicationID == id); 
            if (app == null) return NotFound();
            return View(app);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var app = repository.Applications.FirstOrDefault(a => a.ApplicationID == id);
            if (app != null)
            {
                repository.DeleteApplication(app);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
