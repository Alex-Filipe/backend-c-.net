using Auth.Dtos;
using Auth.Interfaces;
using Auth.Models;

namespace Auth.Services
{
    public class RoleService(IRoleRepository roleRepository)
    {
        private readonly IRoleRepository _roleRepository = roleRepository;


        public void CreateRole(CreateRoleDto role)
        {
            try
            {
                if (_roleRepository.GetRoleByName(role.Name) != null)
                {
                    throw new Exception("Já existe um perfil com este nome.");
                }

                CreateRoleDto newRole = new()
                {
                    Name = role.Name
                };

                _roleRepository.CreateRole(newRole);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Role ShowRole(int roleId)
        {
            try
            {
                var role = _roleRepository.GetRoleById(roleId)  ?? throw new Exception("Perfil não encontrado.");
                return role;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateRole(UpdateRoleDto updatedRole)
        {
            try
            {
                _roleRepository.UpdateRole(updatedRole);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}