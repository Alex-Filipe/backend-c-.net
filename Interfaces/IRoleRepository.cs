using Auth.Dtos;
using Auth.Models;

namespace Auth.Interfaces
{
    public interface IRoleRepository
    {
        void CreateRole(CreateRoleDto newRole);
        Role? GetRoleByName(string roleName);
    }
}