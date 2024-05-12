using Auth.Dtos;
using Auth.Models;

namespace Auth.Interfaces
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);

        List<UserDto> GetAllUsers();

        void CreateUser(User newUser);

        void UpdateUser(UpdateUserDto updatedUser);
        
        void DeleteUser(int id);

        User? GetUserById(int id);

        void UpdateUserPassword(string email, string newPassword);
    }
}