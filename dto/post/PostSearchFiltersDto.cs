namespace SimpleBlogApi.Dto.Post {
  public class PostSearchFiltersDto {

    public int? Page { get; set; }
    public int? PerPage { get; set; }
    public string? Title { get; set; }
    public int? CreatorId { get; set; }
  }
}