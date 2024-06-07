using EmployeeDirectory.Utilities;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.BAL.Providers;
using EmployeeDirectory.BAL.Interfaces;
using EmployeeDirectory.BAL.Extensions;

namespace EmployeeDirectory.Services
{
    public class RoleService(IRoleProvider roleProvider) : IRoleService
    {

        private readonly IRoleProvider _roleProvider = roleProvider;

        public async Task GetRoles()
        {

            List<string> InvalidData = new();
            Display.Print("Enter RoleName");
            string? roleName = Console.ReadLine();
            Display.Print("select department");
            string department= GetDetails("department", DepartmentsProvider.Departments);
            Display.Print("Enter Description");
            string? description = Console.ReadLine();
            Display.Print("Select Location");
            string location = GetDetails("location", LocationProvider.Location);
            BAL.DTO.Role roleInput;
            List<BAL.DTO.Role>? roleData;
            roleData = await _roleProvider.GetRoles();
            if (roleName.IsNullOrEmptyOrWhiteSpace())
            {
                InvalidData.Add("RoleName");
            }
            if (roleData.Any(item => string.Equals(item.Name, department)))
            {
                InvalidData.Add("Department");
            }
            if (roleData.Any(item=> string.Equals(item.Name, location)))
            {
                InvalidData.Add("Location");
            }
            if (InvalidData.Count == 0)
            {
                roleInput = new()
                {
                    Name = roleName,
                    Location = location,
                    Department = department,
                    Description = description
                };
                await _roleProvider.AddRole(roleInput);
                Display.Print("Role Added");
            }
            else
            {
                foreach (string input in InvalidData)
                {
                    Display.Print($"Enter Valid {input}");
                }
                await GetRoles();
            }
        }

        public static string GetDetails(string label, Dictionary<int, string> list)
        {
            foreach (KeyValuePair<int, string> item in list)
            {
                Display.Print(item.Key + " " + item.Value);
            }
            int selectedKey;
            selectedKey = int.Parse(Console.ReadLine()!);
            return list[selectedKey];
        }

        public async Task DisplayRoles()
        {
            List<BAL.DTO.Role>? roleData;
            roleData = await _roleProvider.GetRoles();
            Display.PrintRoleData(roleData);
        }
    }
}