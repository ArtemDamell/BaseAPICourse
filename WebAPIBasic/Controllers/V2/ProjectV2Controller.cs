using AutoMapper;
using Core.DTO;
using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIBasic.Filters;
using WebAPIBasic.QueryFilters;

// 56. Переписатл все методы, чтобы избавиться от грязи в коде
namespace WebAPIBasic.Controllers.V2
{
    // 62.2/62.3 Добавляем атрибут явного указания версии
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/projects")]

    // 120.1 Добавить новый фильтр в контроллер
    //[APIKeyAuthFilter]
    // 136. Заменить на всех контроллерах ApiKeyAuthFilter на CustomeTokenAuthFilterAttribute
    //[CustomeTokenAuthFilter]

    // 177.1 Т.К. у нас уже другой сервер, отвечающий за проверку токенов, в контроллерах меняем наш фильтр ([CustomeTokenAuthFilter]) на простой [Authorize]
    [Authorize]
    public class ProjectV2Controller : ControllerBase
    {

        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public ProjectV2Controller(AppDbContext db, IMapper mapper)
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

        // 105.6 Меняем метод получения
        //[HttpGet]
        //[Route("/api/projects/{pId}/tickets")]
        //public async Task<IActionResult> GetProjectTicket(int pId)
        //{
        //    var tickets = await _db.Tickets.Where(x => x.ProjectId == pId).ToListAsync();

        //    if (tickets is null || !tickets.Any())
        //        return NotFound();
        //    return Ok(tickets);
        //}
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

            // --> После проверки на работоспособность, переходим в ProjectTickets проекта MyApp.Web
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

