using Core.Models;
using MyApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Business
{
    // 94. новый класс для получения билетов проекта и их вывода пользователю TicketsScreen
    public class TicketsScreen : ITicketsScreen
    {
        // 94.1 Внедряем зависимость ProjectRepository
        private readonly IProjectRepository _projectRepository;
        private readonly ITicketRepository _ticketRepository;

        public TicketsScreen(IProjectRepository projectRepository, ITicketRepository ticketRepository)
        {
            _projectRepository = projectRepository;
            _ticketRepository = ticketRepository;
        }

        // 94.2 Создаём метод получения билетов
        public async Task<IEnumerable<Ticket>> ViewTickets(int projectId)
        {
            return await _projectRepository.GetProjectTicketsAsync(projectId);
        }

        // 102. Создать метод запроса билета с фильтром (поиском)
        public async Task<IEnumerable<Ticket>> SearchTickets(string filter)
        {
            // На этом этапе внедряем зависимость TicketRepository
            return await _ticketRepository.GetAsync(filter);

            // Добавляем новый метод в репозиторий, выбрав пункт Pull
        }

        // 105.1 Решаем задачу
        public async Task<IEnumerable<Ticket>> SearchOwnersTicketsAsync(int projectId, string ownerName)
        {
            return await _projectRepository.GetProjectTicketsAsync(projectId, ownerName);
            // --> Далее идём в ProjectRepository и изменяем метод GetProjectTicketsAsync
        }

        // 106.1 Добавляем метод получения билета по ID
        public async Task<Ticket> ViewTicketByIdAsync(int ticketId)
        {
            return await _ticketRepository.GetByIdAsync(ticketId);
            // После этого делаем PULL в интерфейс нового метода
        }

        // 106.2 Добавляем метод обновления билетов
        public async Task UpdateTicketAsync(Ticket ticket)
        {
            await _ticketRepository.UpdateAsync(ticket);
            // После этого делаем PULL в интерфейс нового метода
        }

        // 110. В MyApp.Business добавить новый метод в класс TicketsScreen, AddTicket()
        public async Task<int> CreateTicketAsync(Ticket ticketForCreation)
        {
            return await _ticketRepository.CreateAsync(ticketForCreation);
        }

        // 115. В классе TicketScreen добавить метод удаления билетов
        public async Task DeleteTicketAsync(int ticketId)
        {
            await _ticketRepository.DeleteAsync(ticketId);
            // После этого делаем PULL в интерфейс нового метода
        }
    }
}
