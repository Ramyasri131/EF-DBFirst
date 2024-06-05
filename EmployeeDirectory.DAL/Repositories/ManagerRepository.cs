using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectory.DAL.Repository
{
    public class ManagerRepository(RamyaEmployeeDirectoryDbContext ramyaEmployeeDirectoryDbContext) : IManagerRepository
    {
        private readonly RamyaEmployeeDirectoryDbContext dbContext = ramyaEmployeeDirectoryDbContext;

        public async Task<List<Manager>> GetManagers()
        {
            return await dbContext.Set<Manager>().ToListAsync();
        }
    }
}