using EmployeeDirectory.BAL.Exceptions;
using EmployeeDirectory.BAL.Extensions;
using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.BAL.Interfaces;
using System.Globalization;
using EmployeeDirectory.DAL.Models;

namespace EmployeeDirectory.BAL.Providers
{
    public class EmployeeProvider(IEmployeeRepository employeeRepository,ILocationRepository locationRepository,IDepartmentRepository departmentRepository,IManagerRepository managerRepository,IProjectRepository projectRepository,IRoleRepository roleRepository) : IEmployeeProvider
    {

        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly ILocationRepository _locationRepository = locationRepository;
        private readonly IDepartmentRepository _departmentRepository = departmentRepository;
        private readonly IManagerRepository _managerRepository = managerRepository;
        private readonly IProjectRepository _projectRepository = projectRepository;
        private readonly IRoleRepository _roleRepository = roleRepository;

        public async Task AddEmployee(DTO.Employee employee)
        {
            List<Employee>employees;
            employees = await _employeeRepository.GetAll();
            Employee user=new Employee();
            if (employees.Any(emp=> string.Equals(emp.Email, employee.Email)))
            {
                throw new InvalidData("Employee with mail exists");
            }
            int employeeCount = int.Parse(employees[^1].Id[2..]) + 1;
            string id = string.Format("{0:0000}", employeeCount);
            id = "TZ" + id;
            List<Location> locations = await _locationRepository.GetAll();
            foreach (Location location in locations)
            {
                user.Location = string.Equals(location.Name, employee.Location) ? location.Id : user.Location;
            }
            List<DAL.Models.Department> departments = await _departmentRepository.GetAll();
            foreach (DAL.Models.Department department in departments)
            {
                user.Department = string.Equals(department.Name, employee.Department) ? department.Id : user.Department;
            }
            List<Manager> managers = await _managerRepository.GetAll();
            foreach (Manager manager in managers)
            {
                user.Manager = string.Equals(manager.Name, employee.Manager) ? manager.Id : user.Manager;
            }
            List<Project> projects = await _projectRepository.GetAll();
            foreach (Project project in projects)
            {
                user.Project = string.Equals(project.Name, employee.Project) ? project.Id : user.Project;
            }
            List<Role> roles= await _roleRepository.GetAll();
            foreach (Role role in roles)
            {
                user.JobTitle = string.Equals(role.Name, employee.JobTitle) ? role.Id : user.JobTitle;
            }
            user.Id = id;
            user.FirstName = employee.FirstName!;
            user.LastName = employee.LastName!;
            user.DateOfBirth = DateOnly.ParseExact(employee.DateOfBirth!, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            user.MobileNumber = employee.MobileNumber;
            user.DateOfJoin = DateOnly.ParseExact(employee.DateOfJoin!, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            user.Email = employee.Email!;
            await _employeeRepository.Add(user);
        }

        public async Task<List<BAL.DTO.Employee>> GetEmployees()
        {
            List<Employee> employeeData;
            employeeData = await _employeeRepository.GetAll();
            List<BAL.DTO.Employee> employees= new List<BAL.DTO.Employee>();
            foreach (DAL.Models.Employee employee in employeeData)
            {
                BAL.DTO.Employee employeeInput = new()
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    DateOfBirth = employee.DateOfBirth.ToString(),
                    Email = employee.Email,
                    MobileNumber = employee.MobileNumber,
                    DateOfJoin = employee.DateOfJoin.ToString(),
                    Location = LocationProvider.Location[employee.Location],
                    JobTitle = RoleProvider.Roles[employee.JobTitle],
                    Department = DepartmentsProvider.Departments[employee.Department],
                    Manager = ManagerProvider.Managers[employee.Manager],
                    Project = ProjectsProvider.Projects[employee.Project]
                };
                employees.Add(employeeInput);
            }
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
                DAL.Models.Employee? employee = await _employeeRepository.GetById(id);
                if(employee is null)
                {
                    throw new RecordNotFound("Employee not found");
                }
                else
                {
                    List<Action<Employee, string>> modifyEmployeeData = new()
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
                    await _employeeRepository.Update(employee!);
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
                DAL.Models.Employee? employee = await _employeeRepository.GetById(id);
                if(employee is null)
                {
                    throw new RecordNotFound("Employee not found");
                }
                else
                {
                    await _employeeRepository.Delete(employee);
                }
            }
        }

        public async Task<BAL.DTO.Employee> GetEmployeeById(string? id)
        {
            if (id.IsNullOrEmptyOrWhiteSpace())
            {
                throw new InvalidData("Invalid Employee Id");
            }
            else
            {
                DAL.Models.Employee? employee = await _employeeRepository.GetById(id);
                if (employee is null)
                {
                    throw new RecordNotFound("Employee not found");
                }
                else
                {
                    BAL.DTO.Employee employeeInput = new()
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        DateOfBirth = employee.DateOfBirth.ToString(),
                        Email = employee.Email,
                        MobileNumber = employee.MobileNumber,
                        DateOfJoin = employee.DateOfJoin.ToString(),
                        Location = LocationProvider.Location[employee.Location],
                        JobTitle = RoleProvider.Roles[employee.JobTitle],
                        Department = DepartmentsProvider.Departments[employee.Department],
                        Manager = ManagerProvider.Managers[employee.Manager],
                        Project = ProjectsProvider.Projects[employee.Project]
                    };
                    return employeeInput;
                }
            }
        }
    }
}