
using Auth.Dtos;
using Auth.Models;
using Auth.Services;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthController(AuthService service, UserService userService) : ControllerBase
    {
        private readonly AuthService _authService = service;
        private readonly UserService _userService = userService;

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto autenticacao)
        {
            try 
            {
                var autenticar = _authService.Autenticar(autenticacao);
                return Ok(autenticar);
            }
            catch (Exception e) 
            {
                var errorResponse = new { Message = $"Erro: {e.Message}" };
                return StatusCode(500, errorResponse);
            }
        }

        [HttpPost("reset-password")]
        public IActionResult UpdateForgotPassword([FromBody] UpdatePasswordUserDto forgotPasswordRequest)
        {
            try 
            {
                var updatePasssword = _userService.UpdateForgotPassword(forgotPasswordRequest);
                return Ok(updatePasssword);
            }
            catch (Exception e) 
            {
                var errorResponse = new { Message = $"{e.Message}" };
                return StatusCode(500, errorResponse);
            }
        }
    }
}
