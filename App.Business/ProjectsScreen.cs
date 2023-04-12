using MyApp.Repository;
using Core.Models;

namespace MyApp.Business
{
    public class ProjectsScreen : IProjectsScreen
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectsScreen(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        /// <summary>
        /// Retrieves a list of projects from the repository.
        /// </summary>
        /// <returns>A list of projects.</returns>
        public Task<IEnumerable<Project>> ViewProjects() => _projectRepository.GetAsync();
    }
}
