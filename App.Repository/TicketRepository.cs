using App.Repository.ApiClient;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Repository
{
    // 87.0
    public class TicketRepository
    {
        // 87.1
        private readonly IWebApiExecuter _webApiExecuter;

        public TicketRepository(IWebApiExecuter webApiExecuter)
        {
            _webApiExecuter = webApiExecuter;
        }

        // 87.2 Создаём метод получения всех проектов
        public async Task<IEnumerable<Ticket>> GetAsync(string filter = null)
        {
            // Устанавливаем версию API в строку браузера
            string uri = "api/tickets?api-version=2.0";
            if (!string.IsNullOrWhiteSpace(filter))
                uri += $"&titleorderdescription={filter.Trim()}";

            return await _webApiExecuter.InvokeGet<IEnumerable<Ticket>>(uri);
        }

        // 87.3 Создаём метод получения проекта по ID
        public async Task<Ticket> GetByIdAsync(int id)
        {
            return await _webApiExecuter.InvokeGet<Ticket>($"api/tickets/{id}?api-version=2.0");
        }

        // 87.4 Создаём метод создания проекта
        public async Task<int> CreateAsync(Ticket ticket)
        {
            ticket = await _webApiExecuter.InvokePost("api/tickets?api-version=2.0", ticket);
            return ticket.Id;
        }

        // 87.5 Создаём метод обновления проекта
        public async Task UpdateAsync(Ticket ticket)
        {
            await _webApiExecuter.InvokePut($"api/tickets/{ticket.Id}?api-version=2.0", ticket);
        }

        // 87.6 Создаём метод обновления проекта
        public async Task DeleteAsync(int id)
        {
            await _webApiExecuter.InvokeDelete($"api/tickets/{id}?api-version=2.0");
        }
    }
}

// -->87.7 Далее идём в консольный класс Program и пишем тесты для нового контроллера

