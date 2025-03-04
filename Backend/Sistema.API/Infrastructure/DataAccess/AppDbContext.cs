using Microsoft.EntityFrameworkCore;
using Sistema.API.Entity;

namespace Sistema.API.Infrastructure.DataAccess;

public class AppDbContext : DbContext
{
  public DbSet<User> Users { get; set; }
  public DbSet<Auth> Auths { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlite("Data Source=SistemDb.db");
  }
}

