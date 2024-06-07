using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.BAL.Interfaces;
using EmployeeDirectory.DAL.Models;

namespace EmployeeDirectory.BAL.Providers
{

    public class DepartmentsProvider(IDepartmentRepository DepartmentRepository) : IDepartmentProvider
    {
        public static Dictionary<int, string> Departments = new();
        private readonly IDepartmentRepository _DepartmentRepository = DepartmentRepository;

        public async Task GetDepartments()
        {
            List<Department> departments = await _DepartmentRepository.GetAll();
            foreach (Department department in departments)
            {
                Departments.Add(department.Id, department.Name.ToString()!);
            }
           
        }

    }
}
