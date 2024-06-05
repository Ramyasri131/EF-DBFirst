using EmployeeDirectory.BAL.Exceptions;
using EmployeeDirectory.BAL.Extensions;
using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.BAL.Interfaces;
using System.Globalization;

namespace EmployeeDirectory.BAL.Providers
{
    public class EmployeeProvider(IEmployeeRepository employeeRepository,ILocationRepository locationRepository,IDepartmentRepository departmentRepository,IManagerRepository managerRepository,IProjectRepository projectRepository) : IEmployeeProvider
    {

        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly ILocationRepository _locationRepository = locationRepository;
        private readonly IDepartmentRepository _departmentRepository = departmentRepository;
        private readonly IManagerRepository _managerRepository = managerRepository;
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task AddEmployee(DTO.Employee employee)
        {
            List<DAL.Models.Employee>employees;
            employees = await _employeeRepository.GetEmployees();
            int employeeCount = int.Parse(employees[^1].Id[2..]) + 1;
            string id = string.Format("{0:0000}", employeeCount);
            id = "TZ" + id;
            List<DAL.Models.Location> locations = await _locationRepository.GetLocations();
            foreach (DAL.Models.Location location in locations)
            {
                employee.Location = string.Equals(location.Name, LocationProvider.Location[employee.Location]) ? location.Id : employee.Location;
            }
            List<DAL.Models.Department>  departments= await _departmentRepository.GetDepartments();
            foreach (DAL.Models.Department  department in departments)
            {
                employee.Department=string.Equals(department.Name,DepartmentsProvider.Departments[employee.Department])? department.Id:employee.Department;
            } 
            List<DAL.Models.Manager> managers= await _managerRepository.GetManagers();
            foreach (DAL.Models.Manager manager in managers)
            {
                employee.Manager = string.Equals(manager.Name, ManagerProvider.Managers[employee.Manager]) ? manager.Id : employee.Manager;
            } 
            List<DAL.Models.Project> projects = await _projectRepository.GetProjects();
            foreach (DAL.Models.Project project in projects)
            {
                employee.Project = string.Equals(project.Name, ProjectsProvider.Projects[employee.Project]) ? project.Id : employee.Project;
            }
            DAL.Models.Employee user = new()
            {
                Id = id,
                FirstName = employee.FirstName!,
                LastName = employee.LastName!,
                DateOfBirth = DateOnly.ParseExact(employee.DateOfBirth!, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Manager = employee.Manager,
                MobileNumber = employee.MobileNumber,
                DateOfJoin = DateOnly.ParseExact(employee.DateOfJoin!, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Email = employee.Email!,
                Location = employee.Location,
                JobTitle = employee.JobTitle,
                Department = employee.Department,
                Project = employee.Project,
            };
            await _employeeRepository.AddEmployee(user);
        }

        public async Task<List<DAL.Models.Employee>> GetEmployees()
        {
            List<DAL.Models.Employee> employees;
            employees = await _employeeRepository.GetEmployees();
            return employees;
        }

        public async Task EditEmployee(string selectedData, string? id, int selectedOption)
        {
            if (id.IsNullOrEmptyOrWhiteSpace())
            {
                throw new InvalidData("Enter Employee Id");
            }
            else
            {
                DAL.Models.Employee? employee = await _employeeRepository.GetEmployee(id);
                if(employee is null)
                {
                    throw new RecordNotFound("Employee not found");
                }
                else
                {
                    id = id?.ToUpper();
                    List<Action<DAL.Models.Employee, string>> modifyEmployeeData = new()
                    {
                      (item, selectedData) => item.FirstName = selectedData,
                      (item, selectedData) => item.LastName = selectedData,
                      (item, selectedData) => item.Email = selectedData,
                      (item, selectedData) => item.DateOfBirth = DateOnly.ParseExact(selectedData, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                      (item, selectedData) => item.MobileNumber = long.Parse(selectedData),
                      (item, selectedData) => item.DateOfJoin =DateOnly.ParseExact(selectedData, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                      (item, selectedData) => item.Location = int.Parse(selectedData),
                      (item, selectedData) => item.JobTitle = int.Parse(selectedData),
                      (item, selectedData) => item.Department = int.Parse(selectedData),
                      (item, selectedData) => item.Manager = int.Parse(selectedData),
                      (item, selectedData) => item.Project = int.Parse(selectedData)
                    };
                    modifyEmployeeData[selectedOption - 1](employee!, selectedData);
                    await _employeeRepository.UpdateEmployee(employee!);
                }
            }
        }

        public async Task DeleteEmployee(string? id)
        {
            if (id.IsNullOrEmptyOrWhiteSpace())
            {
                throw new InvalidData("Invalid Employee Id");
            }
            else
            {
                DAL.Models.Employee? employee = await _employeeRepository.GetEmployee(id);
                if(employee is null)
                {
                    throw new RecordNotFound("Employee not found");
                }
                else
                {
                    await _employeeRepository.DeleteEmployee(employee);
                }
            }
        }

        public async Task<DAL.Models.Employee> GetEmployeeById(string? id)
        {
            if (id.IsNullOrEmptyOrWhiteSpace())
            {
                throw new InvalidData("Invalid Employee Id");
            }
            else
            {
                DAL.Models.Employee? employee = await _employeeRepository.GetEmployee(id);
                if (employee is null)
                {
                    throw new RecordNotFound("Employee not found");
                }
                else
                {
                    return employee;
                }
            }
        }
    }
}