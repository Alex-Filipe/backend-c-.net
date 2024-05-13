
using Auth.Models;
using Auth.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Auth.Dtos;

namespace Auth.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class UserController(UserService userService) : ControllerBase
    {
        private readonly UserService _userService = userService;

        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpPost("new_user")]
        public IActionResult CreateUser([FromBody] CreateUserDto user)
        {
            try
            {
                _userService.CreateUser(user);
                return Ok(new { Message = "Usuário criado com sucesso" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpPut("update_user/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UpdateUserDto userUpdateRequest)
        {
            try
            {
                userUpdateRequest.Id = id; 
                _userService.UpdateUser(userUpdateRequest);
                return Ok(new { Message = "Usuário atualizado com sucesso" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpDelete("delete_user/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id);
                return Ok(new { Message = "Usuário excluído com sucesso" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = $"Erro: {e.Message}" });
            }
        }
    }
}
