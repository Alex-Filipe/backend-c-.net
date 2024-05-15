using Auth.Dtos;
using Auth.Models;

namespace Auth.Interfaces
{
    public interface IRoleRepository
    {
        void CreateRole(CreateRoleDto newRole);
        void UpdateRole(UpdateRoleDto updatedRole);
        Role? GetRoleByName(string roleName);
        Role? GetRoleById(int roleId);
    }
}