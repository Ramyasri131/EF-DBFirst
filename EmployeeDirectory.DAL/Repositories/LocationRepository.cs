using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.DAL.Models;
using EmployeeDirectory.DAL.Repositories;


namespace EmployeeDirectory.DAL.Repository
{
    public class LocationRepository(RamyaEmployeeDirectoryDbContext dbContext) : GenericRepository<Location>(dbContext),ILocationRepository
    {
        
    }
}