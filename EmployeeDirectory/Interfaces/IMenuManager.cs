namespace EmployeeDirectory.Interfaces
{
    public interface IMenuManager
    {
        public Task DisplayMainMenu();

        public Task DisplayEmployeeManagementMenu();

        public Task DisplayRoleManagementMenu();
    }
}