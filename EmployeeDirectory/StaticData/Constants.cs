namespace EmployeeDirectory.StaticData
{
    public static class Constants
    {

        public static Dictionary<int, string> EmployeeDataLabels = new()
        {
           {1,"FirstName" },
           {2,"LastName" },
           {3,"Email" },
           {4,"MobileNumber" },
           {5, "DateOfBirth" },
           {6,"DateOfJoin" },
           {7,"Location" },
           {8,"JobTitle" },
           {9,"Department" },
           {10,"Manager" },
           {11,"Project" }
        };

        public static Dictionary<int, string> MainMenu = new()
        {
            {1,"Employee Management" },
            {2,"Role Management" },
            {3,"Exit" }
        };
        
        public static Dictionary<int, string> EmployeeManagementMenu = new()
        {
            {1,"Add employee" },
            {2,"Display All" },
            {3,"Display One"},
            {4,"Edit employee"},
            {5,"Delete employee"},
            {6,"Go Back"}
        };

        public static Dictionary<int, string> RoleManagementMenu = new()
        {
            {1,"Add Role" },
            {2,"Display All" },
            {3,"Go Back" }
        };
    }
}