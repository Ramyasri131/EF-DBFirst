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
               Print($"{propertyInfo.Name}:{propertyInfo.GetValue(employee)}");
            }
            Print("=============================================");
        }

        public static void PrintRoleData(List<BAL.DTO.Role>? roleData)
        {
            foreach (BAL.DTO.Role item in roleData!)
            {
                foreach (PropertyInfo propertyInfo in item.GetType().GetProperties())
                {
                   Print($"{propertyInfo.Name}:{propertyInfo.GetValue(item)}");
                }
                Print("==========================================");
            }
        }
    }
}