using BTL.NET2.Models;
using Microsoft.EntityFrameworkCore;

namespace BTL.NET2.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Comment> Comments { get; set; }
  }
}