using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.DAL.Models;
using EmployeeDirectory.DAL.Repositories;


namespace EmployeeDirectory.DAL.Repository
{
    public class ManagerRepository(RamyaEmployeeDirectoryDbContext dbContext) : GenericRepository<Manager>(dbContext),IManagerRepository
    {
        
    }
}