using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SimpleBlogApi.Models
{
  [Index(nameof(Email), IsUnique = true)]
  public class User
  {
    public Guid Id { get; set; } = Guid.NewGuid();

    [EmailAddress]
    public required string Email { get; set; }
    public required string Password { get; set; }

    public ICollection<Post> Posts { get; set; } = [];
  }
}