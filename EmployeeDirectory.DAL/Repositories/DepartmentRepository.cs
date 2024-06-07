using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.DAL.Models;
using EmployeeDirectory.DAL.Repositories;

namespace EmployeeDirectory.DAL.Repository
{
    public class DepartmentRepository(RamyaEmployeeDirectoryDbContext dbContext) : GenericRepository<Department>(dbContext), IDepartmentRepository
    {

    }
}