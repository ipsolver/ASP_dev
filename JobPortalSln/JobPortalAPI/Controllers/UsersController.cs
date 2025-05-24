using Microsoft.AspNetCore.Mvc;
using JobPortal.Models;
using Microsoft.AspNetCore.Authorization;

namespace JobPortalAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IJobPortalRepository _repo;

        public UsersController(IJobPortalRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_repo.Users.ToList());

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var user = _repo.Users.FirstOrDefault(u => u.UsersID == id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Users user)
        {
            if (_repo.Users.Any(u => u.Login == user.Login))
            {
                return BadRequest("Користувач з таким логіном вже існує.");
            }

            _repo.CreateUser(user);
            return CreatedAtAction(nameof(Get), new { id = user.UsersID }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Users updated)
        {
            var user = _repo.Users.FirstOrDefault(u => u.UsersID == id);
            if (user == null) return NotFound();

            if (_repo.Users.Any(u => u.Login == updated.Login && u.UsersID != id))
            {
                return BadRequest("Логін вже зайнятий іншим користувачем.");
            }

            user.Name = updated.Name;
            user.LastName = updated.LastName;
            user.Login = updated.Login;
            user.Password = updated.Password;
            user.Type_access = updated.Type_access;

            _repo.UpdateUser(user);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var user = _repo.Users.FirstOrDefault(u => u.UsersID == id);
            if (user == null) return NotFound();
            _repo.DeleteUser(user);
            return NoContent();
        }
    }
}
