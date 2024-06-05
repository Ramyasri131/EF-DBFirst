namespace EmployeeDirectory.BAL.Exceptions
{
    public class InvalidData(string message) : Exception(string.Format(message))
    {
    }
}