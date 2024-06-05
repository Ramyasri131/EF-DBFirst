using EmployeeDirectory.BAL.Providers;
using EmployeeDirectory.BAL.DTO;
using System.Reflection;


namespace EmployeeDirectory.Utilities
{
    public static class Display
    {
        public static void Print(string message)
        {
            Console.WriteLine(message);
        }

        public static void Print(int key, string message)
        {
            Console.WriteLine($"{key}.{message}");
        }

        public static void PrintEmployeesData(List<Employee> employeeData)
        {
            foreach (Employee employee in employeeData)
            {
                PrintEmployeeData(employee);
            }
            return;
        }

        public static void PrintEmployeeData(Employee employee)
        {
            foreach (PropertyInfo propertyInfo in employee.GetType().GetProperties())
            {
                if (string.Equals(propertyInfo.Name, "Location"))
                {
                    Display.Print($"{propertyInfo.Name}:{LocationProvider.Location[Convert.ToInt32(propertyInfo.GetValue(employee))]}");
                }
                else if (string.Equals(propertyInfo.Name, "Department"))
                {
                    Display.Print($"{propertyInfo.Name}:{DepartmentsProvider.Departments[Convert.ToInt32(propertyInfo.GetValue(employee))]}");
                }
                else if (string.Equals(propertyInfo.Name, "Manager"))
                {
                    Display.Print($"{propertyInfo.Name}:{ManagerProvider.Managers[Convert.ToInt32(propertyInfo.GetValue(employee))]}");
                }
                else if (string.Equals(propertyInfo.Name, "Project"))
                {
                    Display.Print($"{propertyInfo.Name}:{ProjectsProvider.Projects[Convert.ToInt32(propertyInfo.GetValue(employee))]}");
                }
                else if (string.Equals(propertyInfo.Name, "JobTitle"))
                {
                    Display.Print($"{propertyInfo.Name}:{RoleProvider.Roles[Convert.ToInt32(propertyInfo.GetValue(employee))]}");
                }
                else
                {
                    Display.Print($"{propertyInfo.Name}:{propertyInfo.GetValue(employee)}");
                }
            }
            Display.Print("=============================================");
        }

        public static void PrintRoleData(List<BAL.DTO.Role>? roleData)
        {
            foreach (BAL.DTO.Role item in roleData!)
            {
                foreach (PropertyInfo propertyInfo in item.GetType().GetProperties())
                {
                    if(string.Equals(propertyInfo.Name,"Location"))
                    {
                        Display.Print($"{propertyInfo.Name}:{LocationProvider.Location[Convert.ToInt32(propertyInfo.GetValue(item))]}");
                    }
                    else if(string.Equals(propertyInfo.Name, "Department"))
                    {
                        Display.Print($"{propertyInfo.Name}:{DepartmentsProvider.Departments[Convert.ToInt32(propertyInfo.GetValue(item))]}");
                    }
                    else
                    {
                        Display.Print($"{propertyInfo.Name}:{propertyInfo.GetValue(item)}");
                    }
                }
                Display.Print("==========================================");
            }
        }
    }
}