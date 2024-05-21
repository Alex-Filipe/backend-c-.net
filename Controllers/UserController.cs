using Auth.Models;
using Auth.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Auth.Dtos;
using RabbitMQ.Client; // Importe o namespace do RabbitMQ

namespace Auth.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IConnection _rabbitMQConnection;

        public UserController(UserService userService)
        {
            _userService = userService;

            // Configurar a conexão com o RabbitMQ
            var factory = new ConnectionFactory()
            {
                HostName = "localhost", // Endereço do servidor RabbitMQ
                UserName = "guest",     // Usuário do RabbitMQ
                Password = "guest"      // Senha do RabbitMQ
            };

            _rabbitMQConnection = factory.CreateConnection();
        }

        // Método para obter um canal (channel) do RabbitMQ
        private IModel GetChannel()
        {
            // Criar e retornar um novo canal (channel) a partir da conexão
            return _rabbitMQConnection.CreateModel();
        }

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
                // Obtenha o canal (channel) necessário para criar o usuário
                IModel channel = GetChannel();

                // Chame o método CreateUser passando o usuário e o canal
                _userService.CreateUser(user, channel);
                
                return Ok(new { Message = "Usuário criado com sucesso" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = $"Erro: {e.Message}" });
            }
        }

        // Novo endpoint para criar usuários a partir de mensagens da fila
        [HttpPost("create_user_from_message")]
        public IActionResult CreateUserFromMessage([FromBody] CreateUserDto user)
        {
            try
            {
                _userService.CreateUserr(user);
                return Ok(new { Message = "Usuário criado com sucesso a partir da mensagem da fila" });
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
