namespace EmployeeDirectory.BAL.Interfaces
{
    public interface IRoleProvider
    {
        public Task AddRole(DTO.Role roleInput);

        public Task<List<DTO.Role>> GetRoles();

        public Task GenerateRoleList();
    }
}