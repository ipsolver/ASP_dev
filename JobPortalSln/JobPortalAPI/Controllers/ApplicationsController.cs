using JobPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ApplicationsController : ControllerBase
    {
        private readonly IJobPortalRepository _repo;

        public ApplicationsController(IJobPortalRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var applications = _repo.Applications.ToList();
            return Ok(applications);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var application = _repo.Applications.FirstOrDefault(a => a.ApplicationID == id);
            if (application == null)
                return NotFound();
            return Ok(application);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Application application)
        {
            _repo.CreateApplication(application);
            return CreatedAtAction(nameof(Get), new { id = application.ApplicationID }, application);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Application updated)
        {
            var application = _repo.Applications.FirstOrDefault(a => a.ApplicationID == id);
            if (application == null)
                return NotFound();

            application.CandidateID = updated.CandidateID;
            application.JobID = updated.JobID;
            application.Resume = updated.Resume;
            application.Status = updated.Status;

            _repo.UpdateApplication(application);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var application = _repo.Applications.FirstOrDefault(a => a.ApplicationID == id);
            if (application == null)
                return NotFound();

            _repo.DeleteApplication(application);

            return NoContent();
        }
    }
}
