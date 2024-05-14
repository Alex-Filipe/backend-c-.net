using Auth.Database;
using Auth.Dtos;
using Auth.Interfaces;
using Auth.Models;

namespace Auth.Repositories
{
    public class RoleRepository(ApplicationDbContext context) : IRoleRepository
    {
        private readonly ApplicationDbContext _context = context;

        public Role? GetRoleByName(string roleName)
        {
            return _context.Roles.FirstOrDefault(role => role.Name == roleName);
        }

        public Role? GetRoleById(int roleId)
        {
            return _context.Roles.FirstOrDefault(role => role.Id == roleId);
        }

        public void CreateRole(CreateRoleDto newRole)
        {
            var role = new Role
            {
                Name = newRole.Name
            };

            _context.Roles.Add(role);
            _context.SaveChanges();
        }
    }
}