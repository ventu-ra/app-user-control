using Microsoft.EntityFrameworkCore;

namespace SistemaAPI.Models;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options)
      : base(options) { }

  public DbSet<Usuario> Usuarios { get; set; }
  public DbSet<Login> Logins { get; set; }
}

