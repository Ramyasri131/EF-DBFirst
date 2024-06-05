namespace EmployeeDirectory.Interfaces
{
    public interface IEmployeeService
    {
        public Task GetEmployee();

        public Task DisplayEmployees();

        public Task DisplayEmployee();

        public Task EditEmployee();

        public Task DeleteEmployee();
    }
}