using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace WebAPIBasic.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(policy: "write")]
    public class TicketsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public TicketsController(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Retrieves a list of all tickets from the database.
        /// </summary>
        /// <returns>
        /// A list of all tickets from the database, or a NotFound result if no tickets are found.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Ticket> allTickets = new();

            try
            {
                allTickets = await _db.Tickets.ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            if (allTickets is null)
                return NotFound();

            return Ok(allTickets);
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