using AutoMapper;
using Core.DTO;
using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPIBasic.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(policy: "WebApiScope")]
    public class ProjectController : ControllerBase
    {

        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public ProjectController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        /// <summary>
        /// Gets a list of all projects from the database.
        /// </summary>
        /// <returns>A list of all projects.</returns>
        public async Task<IActionResult> Get()
        {
            return Ok(await _db.Projects.ToListAsync());
        }

        [HttpGet("{id}")]
        /// <summary>
        /// Retrieves a project from the database by its id.
        /// </summary>
        /// <returns>The project with the specified id, or a NotFound result if no project with the specified id exists.</returns>
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _db.Projects.FindAsync(id);

            if (project is null)
                return NotFound();

            return Ok(project);
        }

        [HttpGet]
        [Route("/api/projects/{pId}/tickets")]
        /// <summary>
        /// Retrieves a list of tickets associated with a given project Id.
        /// </summary>
        /// <param name="pId">The project Id.</param>
        /// <returns>A list of tickets associated with the given project Id.</returns>
        public async Task<IActionResult> GetProjectTicket(int pId)
        {
            var tickets = await _db.Tickets.Where(x => x.ProjectId == pId).ToListAsync();

            if (tickets is null || !tickets.Any())
                return NotFound();
            return Ok(tickets);
        }

        [HttpPost]
        /// <summary>
        /// Creates a new project and saves it to the database.
        /// </summary>
        /// <param name="project">The project to be created.</param>
        /// <returns>The created project.</returns>
        public async Task<IActionResult> Post([FromBody] Project project)
        {
            await _db.Projects.AddAsync(project);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),
                new { id = project.Id },
                project);
        }

        [HttpPut("{id}")]
        /// <summary>
        /// Updates a project in the database.
        /// </summary>
        /// <param name="id">The id of the project to update.</param>
        /// <param name="project">The project object to update.</param>
        /// <returns>NoContent if the update was successful, BadRequest if the id does not match the project, NotFound if the project does not exist, or an exception if an error occurs.</returns>
        public async Task<IActionResult> Put(int id, Project project)
        {
            if (!id.Equals(project.Id))
                return BadRequest();

            _db.Entry(project).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (await _db.Projects.FindAsync(id) is null)
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        /// <summary>
        /// Deletes a project from the database.
        /// </summary>
        /// <param name="id">The id of the project to delete.</param>
        /// <returns>The deleted project.</returns>
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _db.Projects.FindAsync(id);

            if (project is null)
                return NotFound();

            _db.Projects.Remove(project);
            await _db.SaveChangesAsync();

            return Ok(project);
        }

        [HttpGet]
        [Route("/api/projects/{pId}/eventadmins")]
        /// <summary>
        /// Gets all Event Administrators associated with a given Project Id.
        /// </summary>
        /// <returns>
        /// Returns a list of EventAdministratorDTO objects associated with the given Project Id.
        /// </returns>
        public async Task<IActionResult> GetAdminsById(int pId)
        {
            try
            {
                var allAdmins = await _db.EventAdministrators.Where(x => x.ProjectId == pId).ToListAsync();
                var dto = _mapper.Map<IEnumerable<EventAdministrator>, IEnumerable<EventAdministratorDTO>>(allAdmins);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

