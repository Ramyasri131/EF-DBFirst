using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.DAL.Models;
using EmployeeDirectory.DAL.Repositories;

namespace EmployeeDirectory.DAL.Data
{
    public class RoleRepository(RamyaEmployeeDirectoryDbContext dbContext) : GenericRepository<Role>(dbContext), IRoleRepository
    {
        
    }
}