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

        public List<AllUserDto> GetAllUsers()
        {
            return [.. _context.Users
                           .Join(_context.Roles, // Join com a tabela Role
                                 user => user.Id_role, // Chave estrangeira User
                                 role => role.Id, // Chave primária Role
                                 (user, role) => new AllUserDto
                                 {
                                     Id = user.Id,
                                     Name = user.Name,
                                     Email = user.Email,
                                     Id_role = user.Id_role,
                                     Name_role = role.Name
                                 })
                           .OrderBy(u => u.Name)];
        }

        public void CreateUser(CreateUserDto newUser)
        {
            var user = new User
            {
                Name = newUser.Name,
                Email = newUser.Email,
                Password = newUser.Password,
                Id_role = newUser.Id_role
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(UpdateUserDto updatedUser)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == updatedUser.Id) ?? throw new ArgumentException("Usuário não encontrado");

            user.Name = updatedUser.Name ?? user.Name;
            user.Email = updatedUser.Email ?? user.Email;
            if (updatedUser.Id_role != 0)
            {
                user.Id_role = updatedUser.Id_role;
            }
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