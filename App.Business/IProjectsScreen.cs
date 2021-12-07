using Core.Models;

namespace MyApp.Business
{
    public interface IProjectsScreen
    {
        Task<IEnumerable<Project>> ViewProjects();
    }
}