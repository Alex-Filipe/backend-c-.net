using Auth.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Auth.Dtos;

namespace Auth.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class RoleController(RoleService roleService) : ControllerBase
    {
        private readonly RoleService _roleService = roleService;


        [HttpPost("new_role")]
        public IActionResult CreateRole([FromBody] CreateRoleDto role)
        {
            try
            {
                _roleService.CreateRole(role);
                return Ok(new { Message = "Perfíl criado com sucesso." });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpGet("view_role/{id}")]
        public IActionResult ShowRole(int id)
        {
            try
            {
                var role = _roleService.ShowRole(id);
                return Ok(role);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = $"Erro: {e.Message}" });
            }
        }
    }
}
