using Auth.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.Database;
public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options){}

    // Tabelas
    public DbSet<User> Users { get; set; }
}
