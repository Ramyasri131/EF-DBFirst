using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectory.DAL.Data
{
    public class EmployeeRepository(RamyaEmployeeDirectoryDbContext ramyaEmployeeDirectoryDbContext) : IEmployeeRepository
    {
        private readonly RamyaEmployeeDirectoryDbContext dbContext = ramyaEmployeeDirectoryDbContext;

        public async Task<List<Employee>> GetEmployees()
        {
            return await dbContext.Employees.OrderBy(x=>x.Id).ToListAsync();
        }

        public async Task<Employee?> GetEmployee(string? id)
        {
            return await dbContext.Employees.FirstOrDefaultAsync(e => string.Equals(e.Id, id));
        }

        public async Task AddEmployee(Employee employee)
        {
            dbContext.Employees.Add(employee);
            await dbContext.SaveChangesAsync();
        }
             
        public async Task UpdateEmployee(Employee employee)
        {
            dbContext.Employees.Update(employee);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteEmployee(Employee employee)
        {
            dbContext.Employees.Remove(employee);
            await dbContext.SaveChangesAsync();
        }
    }
}