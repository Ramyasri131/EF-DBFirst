using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectory.DAL.Repository
{
    public class ProjectRepository(RamyaEmployeeDirectoryDbContext dbContext) : IProjectRepository
    {
        private readonly RamyaEmployeeDirectoryDbContext _dbContext = dbContext;

        public async Task<List<Project>> GetProjects()
        {
            return await _dbContext.Set<Project>().ToListAsync();
        }
    }
}
