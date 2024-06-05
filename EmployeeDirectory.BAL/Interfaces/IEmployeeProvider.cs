namespace EmployeeDirectory.BAL.Interfaces
{
    public interface IEmployeeProvider
    {
        public Task AddEmployee(DTO.Employee employee);

        public Task<List<DAL.Models.Employee>> GetEmployees();

        public Task EditEmployee(string selectedData, string? id, int selectedOption);

        public Task DeleteEmployee(string? id);

        public Task<DAL.Models.Employee> GetEmployeeById(string? id);
    }
}
