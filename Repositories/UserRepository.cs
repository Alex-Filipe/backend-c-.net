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
            return _context.Users
                           .Select(u => new UserDto
                           {
                               Id = u.Id,
                               Name = u.Name,
                               Email = u.Email
                           })
                           .OrderBy(u => u.Name)
                           .ToList();
        }
        
        public void CreateUser(CreateUserDto newUser)
        {
            var user = new User
            {
                Name = newUser.Name,
                Email = newUser.Email,
                Password = newUser.Password
            };

            _context.Users.Add(user);
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
            var user = _context.Users.FirstOrDefault(u => u.Id == id) ?? throw new ArgumentException("Usuário não encontrado"); ;

            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public void UpdateUserPassword(string email, string newPassword)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email) ?? throw new ArgumentException("Usuário não encontrado");
            user.Password = newPassword;
            _context.SaveChanges();
        }
    }
}