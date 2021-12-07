using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using WebAPIBasic.Filters.V2;
using WebAPIBasic.QueryFilters;

// 58. Копируем контроллер для второй версии  
namespace WebAPIBasic.Controllers.V2
{
    // 62.1/62.3 Добавляем атрибут явного указания версии
    [ApiVersion("2.0")]
    [ApiController]
    // 65. Комментирую оригинальный маршрут и указываю версию в новом
    [Route("api/tickets")]
    //[Route("api/v{v:apiVersion}/tickets")]

    // 66. При передачи версии API через строку запроса, просто добавляем к адресу параметр ?api-version=2.0
    public class TicketsV2Controller : ControllerBase
    {
        private readonly AppDbContext _db;
        public TicketsV2Controller(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        // 80.1 Новый фильтр в метод GET в параметр
        public async Task<IActionResult> Get([FromQuery] TicketQueryFilter ticketQueryFilter)
        {
            try
            {
                IQueryable<Ticket> queryTickets = _db.Tickets;
                // 80.2 Добавляем логику фильтра
                if (ticketQueryFilter is not null)
                {
                    if (ticketQueryFilter.Id.HasValue)
                        queryTickets = queryTickets.Where(x => x.Id == ticketQueryFilter.Id);
                    // 87.4 Редактируем TicketV2Controller ******************
                    if (!string.IsNullOrWhiteSpace(ticketQueryFilter.TitleOrDescription))
                        queryTickets = queryTickets.Where(x => x.Title.ToLower().Contains(ticketQueryFilter.TitleOrDescription.ToLower()/*, StringComparison.OrdinalIgnoreCase*/)
                        || x.Description.ToLower().Contains(ticketQueryFilter.TitleOrDescription.ToLower())); // <-- StringComparison.OrdinalIgnoreCase сравнивает строки, как будто они преобразованы в верхний регистр, без учёта культуры
                    //if (!string.IsNullOrWhiteSpace(ticketQueryFilter.Description))
                    //    queryTickets = queryTickets.Where(x => x.Description.ToLower().Contains(ticketQueryFilter.Description.ToLower()/*, StringComparison.OrdinalIgnoreCase*/));
                    // --> 87.5 Возвращаемся в TicketRepository
                    // ******************************************************
                    if (!await queryTickets.AnyAsync())
                        return NotFound();

                    return Ok(await queryTickets.ToListAsync().ConfigureAwait(false));
                }
                // *****************************

                //allTickets = await _db.Tickets.ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }

            //if (allTickets is null)
            //    return NotFound();

            //return Ok(allTickets);

            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Ticket? ticket = new();

            try
            {
                ticket = await _db.Tickets.FindAsync(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            if (ticket is null)
                return NotFound();

            return Ok(ticket);
        }

        [HttpPost]
        // 60.1 Добавляем атрибут
        [Ticket_EnshureDescriptionPresentActionFilter]
        public async Task<IActionResult> Post([FromBody] Ticket ticket)
        {
            if (ticket is null)
                return BadRequest();

            try
            {
                await _db.Tickets.AddAsync(ticket);
                await _db.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById),
                    new { id = ticket.Id }, ticket);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        // 60.1 Добавляем атрибут
        [Ticket_EnshureDescriptionPresentActionFilter]
        public async Task<IActionResult> Put(int id, [FromBody] Ticket ticket)
        {
            if (id != ticket.Id)
                return BadRequest();

            try
            {
                _db.Entry(ticket).State = EntityState.Modified;
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(500);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _db.Tickets.FindAsync(id);

            if (ticket is null)
                return NotFound();

            _db.Tickets.Remove(ticket);
            await _db.SaveChangesAsync();

            return Ok(ticket);
        }
    }
}