using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectory.DAL.Repository
{
    public class DepartmentRepository(RamyaEmployeeDirectoryDbContext ramyaEmployeeDirectoryDbContext) : IDepartmentRepository
    {
        private readonly RamyaEmployeeDirectoryDbContext dbContext = ramyaEmployeeDirectoryDbContext;

        public async Task<List<Department>> GetDepartments()
        {
            return await dbContext.Set<Department>().ToListAsync();
        }
    }
}