using Microsoft.AspNetCore.Mvc;
using JobPortal.Models;
using System.Linq;

namespace JobPortal.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IJobPortalRepository repository;

        public NavigationMenuViewComponent(IJobPortalRepository repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = HttpContext.Request.Query["category"].ToString();

            var categories = repository.Jobs
                .Select(j => j.Category.CategoryName)
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            categories.Insert(0, "Всі");

            return View(categories);

        }
    }
}
