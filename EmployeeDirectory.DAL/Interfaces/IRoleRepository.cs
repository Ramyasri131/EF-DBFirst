namespace EmployeeDirectory.DAL.Interfaces
{
    public interface IRoleRepository
    {
        public Task<List<Models.Role>> GetRoles();

        public Task AddRole(Models.Role role);
    }
}
