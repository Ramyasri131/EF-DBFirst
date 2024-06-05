using EmployeeDirectory.DAL.Models;

namespace EmployeeDirectory.DAL.Interfaces
{
    public interface ILocationRepository
    {
        public Task<List<Location>> GetLocations();
    }
}
