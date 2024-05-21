using Auth.Dtos;
using Auth.Interfaces;
using RabbitMQ.Client;
using System;
using System.Text;
using Newtonsoft.Json;

namespace Auth.Services
{
    public class UserService(IUserRepository userRepository)
    {
        private readonly IUserRepository _userRepository = userRepository;

        public List<AllUserDto> GetAllUsers()
        {
            try
            {
                return _userRepository.GetAllUsers();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void CreateUserr(CreateUserDto user)
        {
            try
            {
                // Verifique se já existe um usuário com o mesmo e-mail
                if (_userRepository.GetUserByEmail(user.Email) != null)
                {
                    throw new ArgumentException("Já existe um usuário com esse e-mail.");
                }

                // Hash da senha antes de armazená-la
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

                // Crie um novo objeto User com os dados fornecidos
                CreateUserDto newUser = new()
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = hashedPassword,
                    Id_role = user.Id_role
                };

                // Chame o método CreateUser no repositório para persistir o novo usuário
                _userRepository.CreateUser(newUser);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void CreateUser(CreateUserDto user, IModel channel)
        {
            try
            {
                // Verifique se já existe um usuário com o mesmo e-mail
                if (_userRepository.GetUserByEmail(user.Email) != null)
                {
                    throw new ArgumentException("Já existe um usuário com esse e-mail.");
                }

                // Hash da senha antes de armazená-la
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

                // Crie um novo objeto User com os dados fornecidos
                var createUserMessage = new CreateUserDto
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = hashedPassword,
                    Id_role = user.Id_role
                };

                // Serializa a mensagem para JSON
                var messageBody = JsonConvert.SerializeObject(createUserMessage);
                
                // Publica a mensagem na fila
                channel.BasicPublish(exchange: "", routingKey: "createUserQueue", basicProperties: null, body: Encoding.UTF8.GetBytes(messageBody));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateUser(UpdateUserDto updatedUser)
        {
            try
            {
                ValidateEmailExist(updatedUser.Email, updatedUser.Id);
                _userRepository.UpdateUser(updatedUser);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

         public void DeleteUser(int id)
        {
            try
            {
                _userRepository.DeleteUser(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void ValidateEmailExist(string email, int userId)
        {
            try
            {
                // Verifique se o email já está cadastrado no sistema, exceto para o próprio usuário
                var existingUserWithEmail = _userRepository.GetUserByEmail(email);
                if (existingUserWithEmail != null && existingUserWithEmail.Id != userId)
                {
                    throw new ArgumentException("O email já está sendo usado por outro usuário.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public object UpdateForgotPassword(UpdatePasswordUserDto forgotPasswordRequest)
        {
            try
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(forgotPasswordRequest.ConfirmPassword);

                _userRepository.UpdateUserPassword(forgotPasswordRequest.Email, hashedPassword);

                return new Dictionary<string, object>
                {
                    { "Success", true },
                    { "Message", "Senha resetada com sucesso" }
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}