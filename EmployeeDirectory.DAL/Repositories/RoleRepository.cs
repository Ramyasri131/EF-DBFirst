using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectory.DAL.Data
{
    public class RoleRepository(RamyaEmployeeDirectoryDbContext ramyaEmployeeDirectoryDbContext) : IRoleRepository
    {
        private readonly RamyaEmployeeDirectoryDbContext _context = ramyaEmployeeDirectoryDbContext;

        public async Task<List<Role>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task AddRole(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
        }
    }
}