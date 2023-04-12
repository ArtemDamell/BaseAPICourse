using AutoMapper;
using Core.DTO;
using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIBasic.Filters;
using WebAPIBasic.QueryFilters;

namespace WebAPIBasic.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/projects")]
    [Authorize(policy: "WebApiScope")]
    public class ProjectV2Controller : ControllerBase
    {

        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public ProjectV2Controller(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a list of all projects from the database.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _db.Projects.ToListAsync());

        /// <summary>
        /// Gets a project by its id.
        /// </summary>
        /// <param name="id">The id of the project.</param>
        /// <returns>The project with the specified id.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _db.Projects.FindAsync(id);

            if (project is null)
                return NotFound();

            return Ok(project);
        }

        /// <summary>
        /// Gets a list of tickets for a given project Id, with optional filtering by owner.
        /// </summary>
        /// <param name="pId">The project Id.</param>
        /// <param name="filter">Optional filter for owner.</param>
        /// <returns>A list of tickets for the given project Id.</returns>
        [HttpGet]
        [Route("/api/projects/{pId:int}/tickets")]
        public async Task<IActionResult> GetProjectTicket(int pId, [FromQuery] ProjectTicketQueryFilter filter)
        {
            IQueryable<Ticket> tickets = _db.Tickets.Where(x => x.ProjectId == pId);

            if (filter is not null && !string.IsNullOrWhiteSpace(filter.Owner))
                tickets = tickets.Where(x => !string.IsNullOrWhiteSpace(x.Owner) && x.Owner.ToLower() == filter.Owner.ToLower());

            var listOfTickets = await tickets.ToListAsync();

            if (listOfTickets is null || !listOfTickets.Any())
                return NotFound();
            return Ok(listOfTickets);
        }

        /// <summary>
        /// Creates a new project and saves it to the database.
        /// </summary>
        /// <param name="project">The project to be created.</param>
        /// <returns>The created project.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Project project)
        {
            await _db.Projects.AddAsync(project);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),
                new { id = project.Id },
                project);
        }

        /// <summary>
        /// Updates a project with the given id.
        /// </summary>
        /// <param name="id">The id of the project to update.</param>
        /// <param name="project">The project object to update.</param>
        /// <returns>NoContent if the project was updated successfully, BadRequest if the id does not match the project, NotFound if the project does not exist, or an exception if an error occurs.</returns>
        [HttpPut("{id}")]
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

        /// <summary>
        /// Deletes a project from the database.
        /// </summary>
        /// <param name="id">The id of the project to delete.</param>
        /// <returns>The deleted project.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _db.Projects.FindAsync(id);

            if (project is null)
                return NotFound();

            _db.Projects.Remove(project);
            await _db.SaveChangesAsync();

            return Ok(project);
        }

        /// <summary>
        /// Gets all Event Administrators associated with a given Project Id.
        /// </summary>
        /// <param name="pId">The Project Id.</param>
        /// <returns>A list of Event Administrator DTOs.</returns>
        [HttpGet]
        [Route("/api/projects/{pId}/eventadmins")]
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
                throw ex;
            }
        }
    }
}

