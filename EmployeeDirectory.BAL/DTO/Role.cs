namespace EmployeeDirectory.BAL.DTO
{
    public class Role
    {
        public required string? Name { get; set; }

        public int Location { get; set; } = 0;

        public int Department { get; set; } = 0;

        public string? Description { get; set; }
    }
}
