using MyApp.Repository;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Business
{
    // 88.1 Создаём класс
    public class ProjectsScreen : IProjectsScreen
    {
        // 88.2 Реализуем
        private readonly IProjectRepository _projectRepository;
        public ProjectsScreen(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<Project>> ViewProjects()
        {
            // --> 88.3 На этом месте извлекаем интерфейсы из Projects и TicketsRepository
            return await _projectRepository.GetAsync();
        }
    }
}
