using Microsoft.AspNetCore.Mvc;
using JobPortal.Models;
using JobPortal.Models.ViewModels;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobPortal.Controllers
{
    public class HomeController : Controller
    {
        private IJobPortalRepository repository;
		private const int PageSize = 2;

		public HomeController(IJobPortalRepository repo)
        {
            repository = repo;
        }

        public IActionResult Index(int page=1)
		{
			var usersAll = repository.Users.AsQueryable();
			int countUsers=usersAll.Count();

			var users = usersAll.OrderBy(u => u.UsersID)
				.Skip((page-1)*PageSize).Take(PageSize).ToList();

			var usersList=new UsersList
			{ 
				Users = users,
                PagingInfo = new PagingInfo
				{
					CurrentPage = page,
					ItemsPerPage = PageSize,
					TotalItems = countUsers
				}

			};

			return View(usersList);
		}
	}
}
