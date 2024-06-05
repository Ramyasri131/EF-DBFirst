using EmployeeDirectory.DAL.Models;

namespace EmployeeDirectory.DAL.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task<List<Employee>> GetEmployees();

        public Task<Employee?> GetEmployee(string? id);

        //Addemp
        public Task InsertEmployee(Employee employee);

        public Task UpdateEmployee(Employee employee);

        public Task DeleteEmployee(Employee employee);

    }
}
