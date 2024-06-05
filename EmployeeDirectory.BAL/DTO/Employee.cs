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

        public int Location { get; set; } 

        public int JobTitle { get; set; } 

        public int Department { get; set; }

        public int Manager { get; set; }

        public int Project { get; set; } 
    }
}