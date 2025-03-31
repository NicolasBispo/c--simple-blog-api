using Microsoft.EntityFrameworkCore;
using SimpleBlogApi.Models;

namespace SimpleBlogApi.Data
{
  public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
  {
    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }
  }
}