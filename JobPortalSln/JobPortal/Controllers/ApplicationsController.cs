using Microsoft.AspNetCore.Mvc;
using JobPortal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


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
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = User.FindFirst(ClaimTypes.Role).Value;

            var apps = repository.Applications
                .Include(a => a.Job)
                .Include(a => a.Candidate);

            if (role == "Manager")
                return View(apps.ToList());

            if (role == "Employee")
                return View(apps.Where(a => a.CandidateID == userId).ToList());

            return Forbid();
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
            if (User.IsInRole("Manager"))
                return Forbid();

            ViewBag.Jobs = new SelectList(repository.Jobs, "JobID", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Application app, IFormFile ResumeFile)
        {
            if (User.IsInRole("Manager"))
                return Forbid();

            ModelState.Remove("Resume");

            // Підставити CandidateID з авторизованого користувача
            app.CandidateID = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (ModelState.IsValid)
            {
                if (ResumeFile != null && ResumeFile.Length > 0)
                {
                    var ext = Path.GetExtension(ResumeFile.FileName).ToLower();
                    var allowed = new[] { ".pdf", ".doc", ".docx" };

                    if (!allowed.Contains(ext))
                    {
                        ModelState.AddModelError("Resume", "Allowed file types: PDF, DOC, DOCX.");
                        ViewBag.Jobs = new SelectList(repository.Jobs, "JobID", "Title", app.JobID);
                        return View(app);
                    }

                    var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadsDir))
                        Directory.CreateDirectory(uploadsDir);

                    var uniqueName = Guid.NewGuid().ToString() + ext;
                    var filePath = Path.Combine(uploadsDir, uniqueName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ResumeFile.CopyTo(stream);
                    }

                    app.Resume = "/uploads/" + uniqueName;
                }

                app.Status = "Pending";
                repository.CreateApplication(app);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Jobs = new SelectList(repository.Jobs, "JobID", "Title", app.JobID);
            return View(app);
        }

        public IActionResult Edit(int id)
        {
            var app = repository.Applications.FirstOrDefault(a => a.ApplicationID == id);
            if (app == null) return NotFound();

            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = User.FindFirst(ClaimTypes.Role).Value;

            if (role == "Employee" && app.CandidateID != userId)
                return Forbid();

            ViewBag.Jobs = new SelectList(repository.Jobs, "JobID", "Title", app.JobID);
            ViewBag.Users = new SelectList(repository.Users, "UsersID", "Login", app.CandidateID);
            ViewBag.IsManager = role == "Manager";
            return View(app);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Application app)
        {
            var role = User.FindFirst(ClaimTypes.Role).Value;

            if (role == "Manager")
            {
                var dbApp = repository.Applications.FirstOrDefault(a => a.ApplicationID == app.ApplicationID);
                if (dbApp == null) return NotFound();

                dbApp.Status = app.Status;
                repository.UpdateApplication(dbApp);
                return RedirectToAction(nameof(Index));
            }

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
            if (User.IsInRole("Manager"))
                return Forbid();

            var app = repository.Applications
                .Include(a => a.Job)
                .Include(a => a.Candidate)
                .FirstOrDefault(a => a.ApplicationID == id);
            if (app == null) return NotFound();

            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (User.IsInRole("Employee") && app.CandidateID != userId)
                return Forbid();

            return View(app);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (User.IsInRole("Manager"))
                return Forbid();

            var app = repository.Applications.FirstOrDefault(a => a.ApplicationID == id);
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (User.IsInRole("Employee") && app.CandidateID != userId)
                return Forbid();

            if (app != null)
                repository.DeleteApplication(app);

            return RedirectToAction(nameof(Index));
        }
}
}
