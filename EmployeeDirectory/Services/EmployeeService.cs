using EmployeeDirectory.Utilities;
using EmployeeDirectory.StaticData;
using EmployeeDirectory.BAL.Validators;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.BAL.Providers;
using EmployeeDirectory.BAL.Interfaces;
using EmployeeDirectory.BAL.Exceptions;

namespace EmployeeDirectory.Services
{
    public class EmployeeService(IEmployeeProvider employeeProvider) : IEmployeeService
    {
        private readonly IEmployeeProvider _employeeProvider = employeeProvider;

        public async Task GetEmployee()
        {
            BAL.DTO.Employee? employeeInput = new BAL.DTO.Employee();
            Display.Print("Enter First Name:");
            employeeInput.FirstName = Console.ReadLine();
            Display.Print("Enter Last Name:");
            employeeInput.LastName = Console.ReadLine();
            Display.Print("Enter Date Of Birth in (dd/mm/yyyy):");
            employeeInput.DateOfBirth = Console.ReadLine();
            Display.Print("Enter Email:");
            employeeInput.Email = Console.ReadLine();
            Display.Print("Enter Mobile Number:");
            employeeInput.MobileNumber = long.Parse(Console.ReadLine()!);
            Display.Print("Enter Date Of Join in (dd/mm/yyyy):");
            employeeInput.DateOfJoin = Console.ReadLine();
            Display.Print("select Location:");
            employeeInput.Location = int.Parse(GetDetails("location", LocationProvider.Location, "Add"));
            Display.Print("select JobTitle:");
            employeeInput.JobTitle = int.Parse(GetDetails("jobTitle", RoleProvider.Roles, "Add"));
            Display.Print("select Department:");
            employeeInput.Department = int.Parse(GetDetails("department", DepartmentsProvider.Departments, "Add"));
            Display.Print("select Manager");
            employeeInput.Manager = int.Parse(GetDetails("Manager", ManagerProvider.Managers, "Add"));
            Display.Print("select Project");
            employeeInput.Project = int.Parse(GetDetails("Project", ProjectsProvider.Projects, "Add"));
            List<string> InvalidInputs = EmployeeValidator.ValidateDetails(employeeInput);
            if (InvalidInputs.Count == 0)
            {
               await _employeeProvider.AddEmployee(employeeInput);
            }
            else
            {
                foreach (string input in InvalidInputs)
                {
                    Display.Print($"Enter Valid {input}");
                }
                await GetEmployee();
            }
        }

        public async Task DisplayEmployees()
        {
            List<DAL.Models.Employee> employeeData = await _employeeProvider.GetEmployees();
            List<BAL.DTO.Employee> employees = new List<BAL.DTO.Employee>();
            foreach(DAL.Models.Employee employee in employeeData)
            {
                BAL.DTO.Employee employeeInput = new()
                {
                    Id=employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    DateOfBirth = employee.DateOfBirth.ToString(),
                    Email = employee.Email,
                    MobileNumber = employee.MobileNumber,
                    DateOfJoin = employee.DateOfJoin.ToString(),
                    Location = employee.Location,
                    JobTitle = employee.JobTitle,
                    Department = employee.Department,
                    Manager = employee.Manager,
                    Project = employee.Project
                };
                employees.Add(employeeInput);
            }
            Display.PrintEmployeesData(employees);
            return;
        }

        public async Task DisplayEmployee()
        {
            Display.Print("Enter Employee Id");
            string? id = Console.ReadLine();
            DAL.Models.Employee employeeData =await _employeeProvider.GetEmployeeById(id) ?? throw new BAL.Exceptions.RecordNotFound("Employee not found");
            BAL.DTO.Employee employeeInput = new()
            {
                Id = employeeData.Id,
                FirstName = employeeData.FirstName,
                LastName = employeeData.LastName,
                DateOfBirth = employeeData.DateOfBirth.ToString(),
                Email = employeeData.Email,
                MobileNumber = employeeData.MobileNumber,
                DateOfJoin = employeeData.DateOfJoin.ToString(),
                Location = employeeData.Location,
                JobTitle = employeeData.JobTitle,
                Department = employeeData.Department,
                Manager = employeeData.Manager,
                Project = employeeData.Project
            };
            Display.PrintEmployeeData(employeeInput);
        }

        public async Task EditEmployee()
        {
            Display.Print("Enter Employee Id");
            string? id = Console.ReadLine();
            id = id!.ToUpper();
            Display.Print("Field to edit");
            foreach (KeyValuePair<int, string> item in Constants.EmployeeDataLabels)
            {
                Display.Print($"{item.Key}.{item.Value}");
            }
            Display.Print("Enter Option");
            int selectedOption;
            selectedOption = int.Parse(Console.ReadLine()!);
            string label = Constants.EmployeeDataLabels[selectedOption];
            string? selectedData;
            if (string.Equals(label, "Location"))
            {
                selectedData = GetDetails(label, LocationProvider.Location, "Edit");
            }
            else if (string.Equals(label, "Department"))
            {
                selectedData = GetDetails(label, DepartmentsProvider.Departments, "Edit");
            }
            else if (string.Equals(label, "JobTitle"))
            {
                selectedData = GetDetails(label, RoleProvider.Roles, "Edit");
            }
            else if (string.Equals(label, "Manager"))
            {
                selectedData = GetDetails(label, ManagerProvider.Managers, "Edit");
            }
            else if (string.Equals(label, "Project"))
            {
                selectedData = GetDetails(label, ProjectsProvider.Projects, "Edit");
            }
            else if (string.Equals(label, "DateOfJoin") || string.Equals(label, "DateOfJoin"))
            {
                selectedData = GetValidDetails(label);
            }
            else
            {
                selectedData = GetValidDetails(label);
            }
            await _employeeProvider.EditEmployee(selectedData, id, selectedOption);
        }

        public static string GetDetails(string label, Dictionary<int, string> list,string operation)
        {
            foreach (KeyValuePair<int, string> item in list)
            {
                Display.Print(item.Key + " " + item.Value);
            }
            string? enteredKey = Console.ReadLine();
            int selectedKey = int.Parse(enteredKey!);
            if (selectedKey <= 0 || selectedKey > list.Count)
            {
                if(string.Equals(operation,"Edit"))
                {
                    EmployeeValidator.ValidateData(label, enteredKey);
                }
            }
            return selectedKey.ToString();
        }

        public static string GetValidDetails(string label)
        {
            Display.Print($"Enter {label}");
            string? inputData = Console.ReadLine()!;
            EmployeeValidator.ValidateData(label,inputData!);
            return inputData;
        }

        public async Task DeleteEmployee()
        {
            Display.Print("Enter Employee Id To Delete");
            string? enteredEmpId = Console.ReadLine();
            try
            {
                await _employeeProvider.DeleteEmployee(enteredEmpId);
            }
            catch (RecordNotFound ex)
            {
                Display.Print(ex.Message);
            }
        }
    }
}