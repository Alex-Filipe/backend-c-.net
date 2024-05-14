using Auth.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.Database;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    // Adciona automatico no created_at e updated_at
    private void AddTimestamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

        var now = DateTime.UtcNow;

        foreach (var entity in entities)
        {
            if (entity.State == EntityState.Added)
            {
                ((BaseEntity)entity.Entity).Created_at = now;
            }
            else if (entity.State == EntityState.Modified)
            {
                ((BaseEntity)entity.Entity).Updated_at = now;
            }
        }
    }

    public override int SaveChanges()
    {
        AddTimestamps();
        return base.SaveChanges();
    }

    // Tabelas
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    // Join
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuração entre User e Role
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey(u => u.Id_role)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
