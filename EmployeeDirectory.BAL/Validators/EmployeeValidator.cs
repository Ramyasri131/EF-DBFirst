using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using EmployeeDirectory.BAL.DTO;
using EmployeeDirectory.BAL.Exceptions;
using EmployeeDirectory.BAL.Extensions;
using EmployeeDirectory.BAL.Providers;

namespace EmployeeDirectory.BAL.Validators
{
    public static class EmployeeValidator
    {
        public static List<string> ValidateDetails(DTO.Employee employee)
        {
            string? option;
            List<string> invalidInputs = [];
            foreach (PropertyInfo propertyInfo in employee.GetType().GetProperties())
            {
                string? input = propertyInfo.GetValue(employee)?.ToString();
                if(propertyInfo.Name!="Id")
                {
                    if (!input.IsNullOrEmptyOrWhiteSpace())
                    {
                        switch (propertyInfo.Name)
                        {
                            case "DateOfBirth":
                                DateTime today = DateTime.Now;
                                if (!DateTime.TryParseExact(employee.DateOfBirth, new string[] { "dd/MM/yyyy"}, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                                {
                                    invalidInputs.Add("date of Birth");
                                }
                                else
                                {
                                    int age = today.Year - DateTime.ParseExact(employee.DateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture).Year;
                                    if (age < 18)
                                    {
                                        invalidInputs.Add("date of Birth");
                                    }
                                }
                                break;
                            case "DateOfJoin":
                                if (!DateTime.TryParseExact(employee.DateOfJoin, new string[] { "dd/MM/yyyy"}, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                                {
                                    invalidInputs.Add("date of Join");
                                }
                                break;
                            case "Email":
                                Regex formatOfEmail = new Regex("^[a-zA-Z0-9._%+-]+@tezo.com$");
                                if (!formatOfEmail.IsMatch(employee.Email!))
                                {
                                    invalidInputs.Add("Email");
                                }
                                break;
                            case "MobileNumber":
                                if (employee.MobileNumber.ToString()!.Length != 10)
                                {
                                    invalidInputs.Add("Mobile Number");
                                }
                                break;
                            case "Location":
                                option = employee.Location;
                                if (LocationProvider.Location.Any(location => string.Equals(location.Key, option)))
                                {
                                    invalidInputs.Add("Location");
                                }
                                break;
                            case "Department":
                                option = employee.Department;
                                if (DepartmentsProvider.Departments.Any(department => string.Equals(department.Key, option)))
                                {
                                    invalidInputs.Add("Department");
                                }
                                break;
                            case "Manager":
                                option = employee.Manager;
                                if (ManagerProvider.Managers.Any(manager => string.Equals(manager.Key, option)))
                                {
                                    invalidInputs.Add("Manager");
                                }
                                break;
                            case "Project":
                                option = employee.Project;
                                if (ProjectsProvider.Projects.Any(project => string.Equals(project.Key, option)))
                                {
                                    invalidInputs.Add("Manager");
                                }
                                break;
                        }
                    }
                    else
                    {
                        invalidInputs.Add(propertyInfo.Name);
                    }
                }
            }
            return invalidInputs;
        }

        public static void ValidateData(string label,string? inputData)
        {
            if(inputData.IsNullOrEmptyOrWhiteSpace())
            {
                throw new Exceptions.InvalidData($"Please Enter {label}");
            }
            switch (label)
            {
                case "DateOfBirth":
                    DateTime val;
                    DateTime today = DateTime.Today;
                    if (!DateTime.TryParseExact(inputData, new string[] { "dd/MM/yyyy"}, CultureInfo.InvariantCulture, DateTimeStyles.None, out val))
                    {
                        throw new BAL.Exceptions.InvalidData("Please enter a valid date of birth");
                    }
                    else
                    {
                        int age = today.Year - DateTime.Parse(inputData).Year;
                        if (age < 18)
                        {
                            throw new Exceptions.InvalidData("Please enter a valid date of birth");
                        }
                    }
                    break;
                case "Email":
                    Regex formatOfEmail = new Regex("^[a-zA-Z0-9._%+-]+@tezo.com$");
                    if (!formatOfEmail.IsMatch(inputData!))
                    {
                        throw new Exceptions.InvalidData("Please enter valid Email");
                    }
                    break;
                case "MobileNumber":
                    if (inputData!.Length != 10 || int.TryParse(inputData, out _))
                    {
                        throw new Exceptions.InvalidData("Please enter valid mobile number");
                    }
                    break;
                case "DateOfJoin":
                    if (!DateTime.TryParseExact(inputData, new string[] { "dd/MM/yyyy"}, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime _))
                    {
                        throw new Exceptions.InvalidData("Please enter valid Joining Date");
                    }
                    break;
                case "Location":
                    throw new Exceptions.InvalidData("Please enter option");
                case "Department":
                    throw new Exceptions.InvalidData("Please enter option");
                case "Manager":
                    throw new Exceptions.InvalidData("Please enter option");
                case "Role":
                    throw new Exceptions.InvalidData("Please enter option");
            }
        }
    }
}