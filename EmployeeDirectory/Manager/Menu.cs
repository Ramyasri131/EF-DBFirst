using EmployeeDirectory.Utilities;
using EmployeeDirectory.StaticData;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.BAL.Exceptions;

namespace EmployeeDirectory.Manager
{
   
    public class Menu(IEmployeeService employeeService, IRoleService roleService) : IMenuManager
    {
        private readonly IEmployeeService _employeeService = employeeService;
        private readonly IRoleService _roleService = roleService;
       
        public async Task DisplayMainMenu()
        {
           
            Display.Print("Main Menu");
            foreach (var item in Constants.MainMenu)
            {
                Display.Print(item.Key, item.Value);
            }
            Display.Print("");
            Display.Print("Enter Your Choice:");
            string? enteredOption = Console.ReadLine();
            try
            {
                int selectedOption = int.Parse(enteredOption!);
                switch (selectedOption)
                {
                    case 1:
                        await DisplayEmployeeManagementMenu();
                        break;
                    case 2:
                        await DisplayRoleManagementMenu();
                        break;
                    case 3:
                        Display.Print("Exit");
                        return;
                    default:
                        Display.Print("Invalid Option");
                        break;
                }
            }
            catch (FormatException ex)
            {
                Display.Print(ex.Message);
                await DisplayMainMenu();
            }
        }

        public async Task DisplayEmployeeManagementMenu()
        {
            Display.Print("Employee Management");
            foreach (var item in Constants.EmployeeManagementMenu)
            {
                Display.Print(item.Key, item.Value);
            }
            Display.Print("");
            Display.Print("Enter your choice:");
            string? enteredValue = Console.ReadLine();
            try
            {
                int selectedOption = int.Parse(enteredValue!);
                switch (selectedOption)
                {
                    case 1:
                        await _employeeService.GetEmployee();
                        return;
                    case 2:
                        await _employeeService.DisplayEmployees();
                        return;
                    case 3:
                        await _employeeService.DisplayEmployee();
                        return;
                    case 4:
                        await _employeeService.EditEmployee();
                        return;
                    case 5:
                        await _employeeService.DeleteEmployee();
                        return;
                    case 6:
                        await DisplayMainMenu();
                        return;
                    default:
                        Display.Print("Enter valid option");
                        break;
                }
            }
            catch(FormatException ex)
            { 
                Display.Print(ex.Message);
            }
            catch(RecordNotFound ex)
            {
                Display.Print(ex.Message);
            }
            catch (InvalidData ex)
            {
                Display.Print(ex.Message);
            }
            finally
            {
                await DisplayEmployeeManagementMenu();
            }
        }

        public async Task DisplayRoleManagementMenu()
        {
            Display.Print("Role Management");
            foreach (var item in Constants.RoleManagementMenu)
            {
                Display.Print(item.Key, item.Value);
            }
            Display.Print("");
            Display.Print("Enter your choice:");
            string? enteredValue = Console.ReadLine();
            try
            {
                int selectedOption = int.Parse(enteredValue!);
                switch (selectedOption)
                {
                    case 1:
                       await _roleService.GetRoles();
                        break;
                    case 2:
                        await _roleService.DisplayRoles();
                        break;
                    case 3:
                        await DisplayMainMenu();
                        return;
                    default:
                        Display.Print("Invalid Option");
                        break;
                }
            }
            catch (FormatException )
            {
                Display.Print("Enter Integer value");
            }
            catch (RecordNotFound ex)
            {
                Display.Print(ex.Message);
            }
            catch (InvalidData ex)
            {
                Display.Print(ex.Message);
            }
            finally
            {
                await DisplayRoleManagementMenu();
            }
        }
    }
}