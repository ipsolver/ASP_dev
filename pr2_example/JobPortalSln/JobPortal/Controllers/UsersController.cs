using Microsoft.AspNetCore.Mvc;
using JobPortal.Models;

namespace JobPortal.Controllers
{
    public class UsersController : Controller
    {
        private readonly IJobPortalRepository repository;

        public UsersController(IJobPortalRepository repo)
        {
            repository = repo;
        }
        public IActionResult Index()
        {
            return View(repository.Users.ToList());
        }
        public IActionResult Details(long id)
        {
            var user = repository.Users.FirstOrDefault(u => u.UsersID == id);
            if (user == null) return NotFound();
            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Users user)
        {
            if (repository.Users.Any(u => u.Login == user.Login))
            {
                ModelState.AddModelError("Login", "Користувач з таким логіном вже існує");
            }
            if (ModelState.IsValid)
            {
                repository.CreateUser(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public IActionResult Edit(long id)
        {
            var user = repository.Users.FirstOrDefault(u => u.UsersID == id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Users user)
        {
            if (repository.Users.Any(u => u.Login == user.Login && u.UsersID != user.UsersID))
            {
                ModelState.AddModelError("Login", "Цей логін вже використовується іншим користувачем");
            }
            if (ModelState.IsValid)
            {
                repository.UpdateUser(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public IActionResult Delete(long id)
        {
            var user = repository.Users.FirstOrDefault(u => u.UsersID == id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            if (id == 9)
            {
                TempData["ErrorMessage"] = "Цього користувача не можна видалити!";
                return RedirectToAction(nameof(Index));
            }

            var user = repository.Users.FirstOrDefault(u => u.UsersID == id);
            if (user != null)
            {
                repository.DeleteUser(user);
            }
            return RedirectToAction(nameof(Index));
        }
}
}
