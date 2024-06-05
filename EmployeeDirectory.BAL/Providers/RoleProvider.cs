using EmployeeDirectory.BAL.Exceptions;
using EmployeeDirectory.BAL.Extensions;
using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.BAL.Interfaces;


namespace EmployeeDirectory.BAL.Providers
{
    public class RoleProvider(IRoleRepository roleRepository) : IRoleProvider
    {
        public static Dictionary<int, string> Roles = new();
        private readonly IRoleRepository _roleRepository = roleRepository;

        public async Task AddRole(DTO.Role roleInput)
        {
            if(roleInput.Name.IsNullOrEmptyOrWhiteSpace())
            {
                throw new InvalidData("Enter Role Name");
            }
            List<DAL.Models.Role> RoleData= await  _roleRepository.GetRoles();
            if (RoleData.Any(role => string.Equals(role.Name,roleInput.Name)))
            {
                throw new InvalidData("Role Exists");
            }
            DAL.Models.Role user = new()
            {
                Name= roleInput.Name!,
                Location= roleInput.Location,
                Department=roleInput.Department,
                Description= roleInput.Description
            };
            await _roleRepository.AddRole(user);
        }

        public  async Task<List<DTO.Role>> GetRoles()
        {
            List<DAL.Models.Role> roles = await _roleRepository.GetRoles();
            List<DTO.Role> roleList = new();
            foreach(DAL.Models.Role item in roles)
            {
                DTO.Role user = new()
                {
                   Name = item.Name,
                   Location = item.Location,
                   Department = item.Department,
                   Description = item.Description
                };

                roleList.Add(user);
            }
             return roleList;
        }

        public async Task GenerateRoleList()
        {
            List<DAL.Models.Role> roles = await _roleRepository.GetRoles() ;
            foreach (DAL.Models.Role role in roles)
            {
                Roles.Add(role.Id, role.Name);
            }
        }

    }
}