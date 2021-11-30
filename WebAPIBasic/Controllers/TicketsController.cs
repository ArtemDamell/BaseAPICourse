﻿using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

// 56. Переписатл все методы, чтобы избавиться от грязи в коде
namespace WebAPIBasic.Controllers
{
    // 62.3/62.3 Добавляем атрибут явного указания версии
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public TicketsController(AppDbContext db)
        {
            _db = db;
        }

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