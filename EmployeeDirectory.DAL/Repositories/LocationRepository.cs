using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectory.DAL.Repository
{
    public class LocationRepository(RamyaEmployeeDirectoryDbContext ramyaEmployeeDirectoryDbContext) : ILocationRepository
    {
        private readonly RamyaEmployeeDirectoryDbContext dbContext = ramyaEmployeeDirectoryDbContext;

        public async Task<List<Location>> GetLocations()
        {
            return await dbContext.Set<Location>().ToListAsync();
        }
    }
}