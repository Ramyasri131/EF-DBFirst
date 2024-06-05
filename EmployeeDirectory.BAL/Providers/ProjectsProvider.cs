using EmployeeDirectory.BAL.Interfaces;
using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectory.BAL.Providers
{
    public class ProjectsProvider(IProjectRepository ProjectRepository) : IProjectProvider
    {
        public static Dictionary<int, string> Projects = new();
        private readonly IProjectRepository _ProjectRepository = ProjectRepository;

        public async Task GetProjects()
        {
            List<Project> projects = await _ProjectRepository.GetProjects();
            foreach (Project project in projects)
            {
                Projects.Add(project.Id, project.Name);
            }
        }
    }
}
