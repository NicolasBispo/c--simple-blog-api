
namespace SimpleBlogApi.Dto.Post
{
  public class PostViewDto
  {
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public int AuthorId { get; set; }
    public string? PublishedDate { get; set; }
    public List<string> Tags { get; set; } = [];
    public bool IsPublished { get; set; }
    public string? ImageUrl { get; set; }
    public required Models.User Creator { get; set; }
  }
}