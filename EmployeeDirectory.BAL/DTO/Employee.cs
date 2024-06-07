namespace EmployeeDirectory.BAL.DTO
{
    public class Employee
    {
        public string? Id {  get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public long MobileNumber { get; set; }

        public string? DateOfBirth { get; set; }

        public string? DateOfJoin { get; set; }

        public string? Location { get; set; } 

        public string? JobTitle { get; set; } 

        public string? Department { get; set; }

        public string? Manager { get; set; }

        public string? Project { get; set; } 
    }
}