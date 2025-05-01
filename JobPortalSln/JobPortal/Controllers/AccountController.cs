using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using JobPortal.Models;

namespace JobPortal.Controllers
{
    public class AccountController : Controller
    {
        private readonly IJobPortalRepository repository;

        public AccountController(IJobPortalRepository repo)
        {
            repository = repo;
        }

        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string login, int password)
        {
            var user = repository.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Невірний логін або пароль");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UsersID.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.Type_access)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Profile");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult Profile()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login");

            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = repository.Users.FirstOrDefault(u => u.UsersID == userId);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Profile(Users userModel)
        {
            var user = repository.Users.FirstOrDefault(u => u.UsersID == userModel.UsersID);
            if (user == null) return NotFound();

            user.Name = userModel.Name;
            user.LastName = userModel.LastName;
            user.PhoneNumber = userModel.PhoneNumber;
            user.City = userModel.City;
            user.Description = userModel.Description;

            repository.UpdateUser(user);
            ViewBag.Message = "Дані оновлено успішно!";
            return View(user);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Users user)
        {
            if (repository.Users.Any(u => u.Login == user.Login))
            {
                ModelState.AddModelError("Login", "Користувач з таким логіном вже існує");
            }

            if (ModelState.IsValid)
            {
                repository.CreateUser(user);
                return RedirectToAction("Login");
            }

            return View(user);
        }
    }
}
