using MyApp.Repository.ApiClient;
using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IWebApiExecuter _webApiExecuter;

        public ProjectRepository(IWebApiExecuter webApiExecuter)
        {
            _webApiExecuter = webApiExecuter;
        }

        /// <summary>
        /// Gets a list of projects asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation of getting a list of projects.</returns>
        public Task<IEnumerable<Project>> GetAsync()
        {
            return _webApiExecuter.InvokeGet<IEnumerable<Project>>("api/projects?api-version=2.0");
        }

        /// <summary>
        /// Retrieves a project by its id.
        /// </summary>
        /// <param name="id">The id of the project to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the project.</returns>
        public Task<Project> GetByIdAsync(int id)
        {
            return _webApiExecuter.InvokeGet<Project>($"api/projects/{id}?api-version=2.0");
        }

        /// <summary>
        /// Gets the project tickets asynchronously.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task<IEnumerable<Ticket>> GetProjectTicketsAsync(int projectId, string? filter = null)
        {
            string uri = $"api/projects/{projectId}/tickets";

            if (!string.IsNullOrWhiteSpace(filter))
                uri += $"?Owner={filter}&api-version=2.0";
            else
                uri += "?api-version=2.0";

            return _webApiExecuter.InvokeGet<IEnumerable<Ticket>>(uri);
        }

        /// <summary>
        /// Creates a new project asynchronously.
        /// </summary>
        /// <param name="project">The project to create.</param>
        /// <returns>The Id of the created project.</returns>
        public async Task<int> CreateAsync(Project project)
        {
            project = await _webApiExecuter.InvokePost("api/projects?api-version=2.0", project);
            return project.Id;
        }

        /// <summary>
        /// Updates the project asynchronously.
        /// </summary>
        /// <param name="project">The project to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task UpdateAsync(Project project) => _webApiExecuter.InvokePut($"api/projects/{project.Id}?api-version=2.0", project);

        /// <summary>
        /// Deletes a project with the specified id.
        /// </summary>
        /// <param name="id">The id of the project to delete.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        public Task DeleteAsync(int id) => _webApiExecuter.InvokeDelete($"api/projects/{id}?api-version=2.0");
    }
}

