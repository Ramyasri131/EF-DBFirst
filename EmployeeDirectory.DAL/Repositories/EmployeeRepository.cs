using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.DAL.Models;
using EmployeeDirectory.DAL.Repositories;

namespace EmployeeDirectory.DAL.Data
{
    public class EmployeeRepository(RamyaEmployeeDirectoryDbContext dbContext) : GenericRepository<Employee>(dbContext), IEmployeeRepository
    {
      
    }
}