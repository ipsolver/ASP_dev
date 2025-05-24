using Microsoft.AspNetCore.Mvc;
using JobPortal.Models;
using System.Security.Claims;

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
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userRole = User.FindFirst(ClaimTypes.Role).Value;

            if (userRole == "Manager" || userRole == "Admin")
            {
                return View(repository.Users.ToList());
            }
            else if (userRole == "Employee")
            {
                var currentUser = repository.Users.FirstOrDefault(u => u.UsersID == userId);
                return View(new List<Users> { currentUser });
            }

            return Forbid();
        }

        public IActionResult Details(long id)
        {
            var user = repository.Users.FirstOrDefault(u => u.UsersID == id);
            if (user == null) return NotFound();
            return View(user);
        }

        public IActionResult Create()
        {
            if (User.IsInRole("Manager"))
                return Forbid();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Users user)
        {
            if (User.IsInRole("Manager"))
                return Forbid();

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
            if (User.IsInRole("Manager"))
                return Forbid();

            var user = repository.Users.FirstOrDefault(u => u.UsersID == id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Users user)
        {
            if (User.IsInRole("Manager"))
                return Forbid();

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
            if (User.IsInRole("Manager") || User.IsInRole("Employee"))
                return Forbid();

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

            if (User.IsInRole("Manager") || User.IsInRole("Employee"))
                return Forbid();

            if (User.IsInRole("Manager") || User.IsInRole("Employee"))
                return Forbid();

            var user = repository.Users.FirstOrDefault(u => u.UsersID == id);
            if (user != null)
            {
                repository.DeleteUser(user);
            }
            return RedirectToAction(nameof(Index));
        }
}
}
