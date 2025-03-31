
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleBlogApi.Models
{
  public class Post
  {
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string  Title { get; set; }
    public required string Content { get; set; }
    public int AuthorId { get; set; }
    public string? PublishedDate { get; set; }
    public List<string> Tags { get; set; } = [];
    public bool IsPublished { get; set; }
    public string? ImageUrl { get; set; }
  
    public required int CreatorId {get; set;}
    public required User Creator { get; set; }
  }
}