using Auth.Database;
using Auth.Dtos;
using Auth.Interfaces;
using Auth.Models;

namespace Auth.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        private readonly ApplicationDbContext _context = context;

        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public List<UserDto> GetAllUsers()
        {
            // Projeção para selecionar apenas os campos desejados da entidade User
            return _context.Users
                           .Select(u => new UserDto
                           {
                               Id = u.Id,
                               Name = u.Name,
                               Email = u.Email
                           })
                           .ToList();
        }
        public void CreateUser(User newUser)
        {
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public void UpdateUser(UpdateUserDto updatedUser)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == updatedUser.Id) ?? throw new ArgumentException("Usuário não encontrado");

            user.Name = updatedUser.Name ?? user.Name;
            user.Email = updatedUser.Email ?? user.Email;
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Usuário não encontrado");
            }
        }

        public User? GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public void UpdateUserPassword(string email, string newPassword)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email) ?? throw new ArgumentException("Usuário não encontrado");
            user.Password = newPassword;
            _context.SaveChanges();
        }
    }
}