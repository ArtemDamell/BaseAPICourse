using MyApp.Repository.ApiClient;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Repository
{
    // 87.1 Создаём класс репозитория для билетов
    public class TicketRepository : ITicketRepository
    {
        // 87.2 Внедряем зависимость класса Executer
        private readonly IWebApiExecuter _webApiExecuter;

        public TicketRepository(IWebApiExecuter webApiExecuter)
        {
            _webApiExecuter = webApiExecuter;
        }

        // 87.2 Создаём метод получения всех проектов
        public async Task<IEnumerable<Ticket>> GetAsync(string filter = null)
        {
            // --> 87.3 После создания этого метода, редактируем класс TicketQueryFilter

            // 87.5 Реализовываем метод
            string uri = "api/tickets?api-version=2.0";
            if (!string.IsNullOrWhiteSpace(filter))
                uri += $"&titleordescription={filter.Trim()}";

            return await _webApiExecuter.InvokeGet<IEnumerable<Ticket>>(uri);
        }

        // 87.6 Создаём метод получения билетов по ID
        public async Task<Ticket> GetByIdAsync(int id)
        {
            return await _webApiExecuter.InvokeGet<Ticket>($"api/tickets/{id}?api-version=2.0");
        }



        // 87.7 Создаём метод создания билетов
        public async Task<int> CreateAsync(Ticket ticket)
        {
            ticket = await _webApiExecuter.InvokePost("api/tickets?api-version=2.0", ticket);
            return ticket.Id;
        }

        // 87.8 Создаём метод обновления билетов
        public async Task UpdateAsync(Ticket ticket)
        {
            await _webApiExecuter.InvokePut($"api/tickets/{ticket.Id}?api-version=2.0", ticket);
        }

        // 87.9 Создаём метод обновления билетов
        public async Task DeleteAsync(int id)
        {
            await _webApiExecuter.InvokeDelete($"api/tickets/{id}?api-version=2.0");
        }
    }
}

// -->87.7 Далее идём в консольный класс Program и пишем тесты для нового контроллера

