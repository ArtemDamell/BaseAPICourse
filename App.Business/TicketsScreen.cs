using Core.Models;
using MyApp.Repository;

namespace MyApp.Business
{
    public class TicketsScreen : ITicketsScreen
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITicketRepository _ticketRepository;

        public TicketsScreen(IProjectRepository projectRepository, ITicketRepository ticketRepository)
        {
            _projectRepository = projectRepository;
            _ticketRepository = ticketRepository;
        }

        /// <summary>
        /// Retrieves a list of tickets associated with a given project.
        /// </summary>
        /// <param name="projectId">The ID of the project to retrieve tickets for.</param>
        /// <returns>A list of tickets associated with the given project.</returns>
        public Task<IEnumerable<Ticket>> ViewTickets(int projectId) => _projectRepository.GetProjectTicketsAsync(projectId);

        /// <summary>
        /// Searches tickets using the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>A collection of tickets.</returns>
        public Task<IEnumerable<Ticket>> SearchTickets(string filter) => _ticketRepository.GetAsync(filter);

        /// <summary>
        /// Retrieves a collection of tickets for a given project and owner name.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="ownerName">The owner name.</param>
        /// <returns>
        /// A collection of tickets for a given project and owner name.
        /// </returns>
        public Task<IEnumerable<Ticket>> SearchOwnersTicketsAsync(int projectId, string ownerName) => _projectRepository.GetProjectTicketsAsync(projectId, ownerName);

        /// <summary>
        /// Retrieves a ticket by its Id asynchronously.
        /// </summary>
        /// <param name="ticketId">The Id of the ticket to retrieve.</param>
        /// <returns>A Task containing the retrieved ticket.</returns>
        public Task<Ticket> ViewTicketByIdAsync(int ticketId) => _ticketRepository.GetByIdAsync(ticketId);

        /// <summary>
        /// Updates a ticket asynchronously.
        /// </summary>
        /// <param name="ticket">The ticket to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task UpdateTicketAsync(Ticket ticket) => _ticketRepository.UpdateAsync(ticket);

        /// <summary>
        /// Creates a new ticket asynchronously.
        /// </summary>
        /// <param name="ticketForCreation">The ticket to be created.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task<int> CreateTicketAsync(Ticket ticketForCreation) => _ticketRepository.CreateAsync(ticketForCreation);

        /// <summary>
        /// Deletes a ticket with the specified ticketId asynchronously.
        /// </summary>
        /// <param name="ticketId">The ticketId of the ticket to delete.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        public Task DeleteTicketAsync(int ticketId) => _ticketRepository.DeleteAsync(ticketId);
    }
}
