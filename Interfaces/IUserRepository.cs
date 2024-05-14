using Auth.Dtos;
using Auth.Models;

namespace Auth.Interfaces
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);

        List<AllUserDto> GetAllUsers();

        void CreateUser(CreateUserDto newUser);

        void UpdateUser(UpdateUserDto updatedUser);
        
        void DeleteUser(int id);

        void UpdateUserPassword(string email, string newPassword);
    }
}