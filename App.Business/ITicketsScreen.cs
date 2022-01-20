using Core.Models;

namespace MyApp.Business
{
    // 95. Интерфейс из класса TicketsScreen
    public interface ITicketsScreen
    {
        Task<int> CreateTicketAsync(Ticket ticketForCreation);
        Task DeleteTicketAsync(int ticketId);
        Task<IEnumerable<Ticket>> SearchOwnersTicketsAsync(int projectId, string ownerName);
        Task<IEnumerable<Ticket>> SearchTickets(string filter);
        Task UpdateTicketAsync(Ticket ticket);
        Task<Ticket> ViewTicketByIdAsync(int ticketId);
        Task<IEnumerable<Ticket>> ViewTickets(int projectId);
    }
}