using EmployeeDirectory.DAL.Models;

namespace EmployeeDirectory.DAL.Interfaces
{
    public interface IProjectRepository
    {
        public Task<List<Project>> GetProjects();
    }
}