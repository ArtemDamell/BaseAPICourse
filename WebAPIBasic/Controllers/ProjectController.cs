using AutoMapper;
using Core.DTO;
using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIBasic.Filters;

// 56. Переписатл все методы, чтобы избавиться от грязи в коде
namespace WebAPIBasic.Controllers
{
    // 62.2/62.3 Добавляем атрибут явного указания версии
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]
    // 120.2 Добавить новый фильтр в контроллер
    [APIKeyAuthFilter]
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
        public async Task<IActionResult> Get()
        {
            return Ok(await _db.Projects.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _db.Projects.FindAsync(id);

            if (project is null)
                return NotFound();

            return Ok(project);
        }

        [HttpGet]
        [Route("/api/projects/{pId}/tickets")]
        public async Task<IActionResult> GetProjectTicket(int pId)
        {
            var tickets = await _db.Tickets.Where(x => x.ProjectId == pId).ToListAsync();

            if (tickets is null || !tickets.Any())
                return NotFound();
            return Ok(tickets);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Project project)
        {
            await _db.Projects.AddAsync(project);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),
                new { id = project.Id },
                project);
        }

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

        // 75. Добавить в ProjectsController логику получения всех админов по ID мироприятия
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

