using Auth.Models;

namespace Auth.Interfaces
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);

        void CreateUser(User newUser);

        void UpdateUser(UpdateUserRequest updatedUser);

        User? GetUserById(int id);

        void UpdateUserPassword(string email, string newPassword);
    }
}