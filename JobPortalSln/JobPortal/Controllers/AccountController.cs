using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using JobPortal.Models;
using JobPortal.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

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
            HttpContext.Session.SetString("UserID", user.UsersID.ToString());


            return RedirectToAction("Profile");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult Profile()
        {
            var userId = GetCurrentUserId();

            var user = repository.Users.FirstOrDefault(u => u.UsersID == userId);
            var selectedCategories = repository.UserCategories
                .Where(uc => uc.UsersID == userId)
                .Select(uc => uc.CategoryID)
                .ToList();

            var model = new ProfileViewModel
            {
                User = user,
                AllCategories = repository.Categories.ToList(),
                SelectedCategoryIDs = selectedCategories
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Profile(ProfileViewModel model)
        {
            var user = repository.Users.FirstOrDefault(u => u.UsersID == model.User.UsersID);

            if (user == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                model.AllCategories = repository.Categories.ToList();
                model.SelectedCategoryIDs = repository.UserCategories
                    .Where(uc => uc.UsersID == model.User.UsersID)
                    .Select(uc => uc.CategoryID)
                    .ToList();

                return View(model);
            }

            user.Name = model.User.Name;
            user.LastName = model.User.LastName;
            user.PhoneNumber = model.User.PhoneNumber;
            user.City = model.User.City;
            user.Description = model.User.Description;

            var oldCategories = repository.UserCategories.Where(uc => uc.UsersID == user.UsersID).ToList();
            repository.RemoveUserCategories(oldCategories);

            if (model.SelectedCategoryIDs != null)
            {
                foreach (var categoryId in model.SelectedCategoryIDs)
                {
                    repository.AddUserCategory(new UserCategory
                    {
                        UsersID = user.UsersID.Value,
                        CategoryID = categoryId
                    });
                }
            }

            repository.SaveChanges();

            ViewBag.Message = "Профіль оновлено";

            model.AllCategories = repository.Categories.ToList();
            model.SelectedCategoryIDs = repository.UserCategories
                .Where(uc => uc.UsersID == user.UsersID)
                .Select(uc => uc.CategoryID)
                .ToList();

            return View(model);
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

        private long GetCurrentUserId()
        {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    throw new Exception("Користувач не автентифікований");
                }

                return long.Parse(userIdClaim.Value);

        }

        public IActionResult Notifications()
        {
            var userId = GetCurrentUserId();

            var notifications = repository.Notifications
                .Where(n => n.UsersID == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToList();

            foreach (var notification in notifications.Where(n => !n.IsRead))
            {
                notification.IsRead = true;
            }
            repository.SaveChanges();

            return View(notifications);
        }


    }
}
