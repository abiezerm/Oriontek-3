using Microsoft.EntityFrameworkCore;

namespace server.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}
    public DbSet<Company> Companies { get; set; }
    public DbSet<Employee> Employees { get; set; }

  }
}
