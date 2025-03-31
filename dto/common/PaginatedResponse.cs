namespace SimpleBlogApi.Dto.Common
{
  public class PaginatedResponse<T>
  {


    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PerPage { get; set; }
    public IEnumerable<T> Results { get; set; } = [];
  }
}