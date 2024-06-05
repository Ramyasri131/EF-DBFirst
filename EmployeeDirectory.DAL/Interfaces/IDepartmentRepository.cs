using EmployeeDirectory.DAL.Models;

namespace EmployeeDirectory.DAL.Interfaces
{
    public interface IDepartmentRepository
    {
        public Task<List<Department>> GetDepartments();

    }
}
