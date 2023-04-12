using MyApp.Repository.ApiClient;
using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly IWebApiExecuter _webApiExecuter;

        public TicketRepository(IWebApiExecuter webApiExecuter)
        {
            _webApiExecuter = webApiExecuter;
        }

        /// <summary>
        /// Gets a list of tickets from the API.
        /// </summary>
        /// <param name="filter">Optional filter to search for tickets.</param>
        /// <returns>A list of tickets.</returns>
        public Task<IEnumerable<Ticket>> GetAsync(string? filter = null)
        {
            string uri = "api/tickets?api-version=2.0";
            if (!string.IsNullOrWhiteSpace(filter))
                uri += $"&titleordescription={filter.Trim()}";

            return _webApiExecuter.InvokeGet<IEnumerable<Ticket>>(uri);
        }

        /// <summary>
        /// Retrieves a ticket by its id.
        /// </summary>
        /// <param name="id">The id of the ticket to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation of retrieving the ticket.</returns>
        public Task<Ticket> GetByIdAsync(int id) => _webApiExecuter.InvokeGet<Ticket>($"api/tickets/{id}?api-version=2.0");

        /// <summary>
        /// Creates a new ticket asynchronously.
        /// </summary>
        /// <param name="ticket">The ticket to create.</param>
        /// <returns>The Id of the created ticket.</returns>
        public async Task<int> CreateAsync(Ticket ticket)
        {
            ticket = await _webApiExecuter.InvokePost("api/tickets?api-version=2.0", ticket);
            return ticket.Id;
        }

        /// <summary>
        /// Updates a ticket asynchronously.
        /// </summary>
        /// <param name="ticket">The ticket to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task UpdateAsync(Ticket ticket) => _webApiExecuter.InvokePut($"api/tickets/{ticket.Id}?api-version=2.0", ticket);

        /// <summary>
        /// Deletes a ticket with the specified id.
        /// </summary>
        /// <param name="id">The id of the ticket to delete.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        public Task DeleteAsync(int id) => _webApiExecuter.InvokeDelete($"api/tickets/{id}?api-version=2.0");
    }
}