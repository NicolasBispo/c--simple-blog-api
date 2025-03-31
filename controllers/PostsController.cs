using Microsoft.AspNetCore.Mvc;
using SimpleBlogApi.Dto.Post;
using SimpleBlogApi.Services;

namespace SimpleBlogApi.Controllers
{
  [ApiController]
  [Route("posts")]
  public class PostsController(PostsService postService) : ControllerBase
  {
    private readonly PostsService _postService = postService;

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] PostSearchFiltersDto postSearchFiltersDto)
    {
      var posts = await _postService.ListPosts(postSearchFiltersDto);
      return Ok(posts);
    }
  }
}