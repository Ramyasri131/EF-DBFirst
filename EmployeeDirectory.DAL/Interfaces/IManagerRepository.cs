using EmployeeDirectory.DAL.Models;

namespace EmployeeDirectory.DAL.Interfaces
{
    public interface IManagerRepository
    {
        public Task<List<Manager>> GetManagers();

    }
}