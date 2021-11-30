using App.Repository.ApiClient;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Repository
{
    // 84. Создаём класс для доступа к проектам
    // Теперь нам надо использовать ранее созданный класс. Мы можем его использовать
    // Напрямую, или же через интерфейс. Переходим в WebApiExecuter, нажимаем на название класса ПКМ
    // И экстрактируем Interface (Extract Interface) и нажимаем OK
    public class ProjectRepository
    {
        private readonly IWebApiExecuter _webApiExecuter;

        // 84.1 Внедряем зависимость WebApiExecuter через интерфейс IWebApiExecuter
        // Таким образом мы следуем основному принципу паттерна внедрения зависимостей,
        // Который гласит, что объекты высокого уровня не должны зависить от объектов уровнем ниже
        public ProjectRepository(IWebApiExecuter webApiExecuter)
        {
            _webApiExecuter = webApiExecuter;
        }

        // 84.2 Создаём метод получения всех проектов
        public async Task<IEnumerable<Project>> GetAsync()
        {
            return await _webApiExecuter.InvokeGet<IEnumerable<Project>>("api/project");
        }

        // 84.3 Создаём метод получения проекта по ID
        public async Task<Project> GetByIdAsync(int id)
        {
            return await _webApiExecuter.InvokeGet<Project>($"api/project/{id}");
        }

        // 84.4 Создаём метод получения всех билетов в проекте по ID проекта
        public async Task<IEnumerable<Ticket>> GetProjectTicketsAsync(int projectId)
        {
            return await _webApiExecuter.InvokeGet<IEnumerable<Ticket>>($"api/projects/{projectId}/tickets");
        }

        // 84.5 Создаём метод создания проекта
        public async Task<int> CreateAsync(Project project)
        {
            project = await _webApiExecuter.InvokePost("api/project", project);
            return project.Id;
        }

        // 84.6 Создаём метод обновления проекта
        public async Task UpdateAsync(Project project)
        {
            await _webApiExecuter.InvokePut($"api/project/{project.Id}", project);
        }

        // 84.7 Создаём метод обновления проекта
        public async Task DeleteAsync(int id)
        {
            await _webApiExecuter.InvokeDelete($"api/project/{id}");
        }
    }
}

