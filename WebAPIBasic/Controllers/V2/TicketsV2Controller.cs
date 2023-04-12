using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebAPIBasic.Filters.V2;
using WebAPIBasic.QueryFilters;

namespace WebAPIBasic.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/tickets")]
    [Authorize]
    public class TicketsV2Controller : ControllerBase
    {
        private readonly AppDbContext _db;
        public TicketsV2Controller(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Gets a list of tickets based on the query filter.
        /// </summary>
        /// <param name="ticketQueryFilter">The query filter.</param>
        /// <returns>A list of tickets.</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TicketQueryFilter ticketQueryFilter)
        {
            try
            {
                IQueryable<Ticket> queryTickets = _db.Tickets;
                if (ticketQueryFilter is not null)
                {
                    if (ticketQueryFilter.Id.HasValue)
                        queryTickets = queryTickets.Where(x => x.Id == ticketQueryFilter.Id);
                    if (!string.IsNullOrWhiteSpace(ticketQueryFilter.TitleOrDescription))
                        queryTickets = queryTickets.Where(x => x.Title.ToLower().Contains(ticketQueryFilter.TitleOrDescription.ToLower()/*, StringComparison.OrdinalIgnoreCase*/)
                        || x.Description.ToLower().Contains(ticketQueryFilter.TitleOrDescription.ToLower()));

                    return Ok(await queryTickets.ToListAsync().ConfigureAwait(false));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
            return BadRequest();
        }

        /// <summary>
        /// Gets a ticket by its id.
        /// </summary>
        /// <param name="id">The id of the ticket.</param>
        /// <returns>The ticket with the specified id.</returns>
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

        /// <summary>
        /// Creates a new ticket in the database.
        /// </summary>
        /// <param name="ticket">The ticket to be created.</param>
        /// <returns>The created ticket.</returns>
        [HttpPost]
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

        /// <summary>
        /// Updates a ticket in the database.
        /// </summary>
        /// <param name="id">The id of the ticket to update.</param>
        /// <param name="ticket">The ticket object to update.</param>
        /// <returns>NoContent if successful, BadRequest if the id does not match the ticket, or StatusCode(500) if an exception occurs.</returns>
        [HttpPut("{id}")]
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

        /// <summary>
        /// Deletes a ticket from the database.
        /// </summary>
        /// <param name="id">The id of the ticket to delete.</param>
        /// <returns>The deleted ticket.</returns>
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