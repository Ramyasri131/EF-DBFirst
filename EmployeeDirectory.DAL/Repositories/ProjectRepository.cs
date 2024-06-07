using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.DAL.Models;
using EmployeeDirectory.DAL.Repositories;

namespace EmployeeDirectory.DAL.Repository
{
    public class ProjectRepository(RamyaEmployeeDirectoryDbContext dbContext) : GenericRepository<Project>(dbContext),IProjectRepository
    {
    }
}
