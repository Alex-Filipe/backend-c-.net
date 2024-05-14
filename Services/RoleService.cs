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
                // Verifique se já existe um perfil com este nome
                if (_roleRepository.GetRoleByName(role.Name) != null)
                {
                    throw new ArgumentException("Já existe um perfil com este nome.");
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
    }
}