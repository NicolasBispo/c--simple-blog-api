using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleBlogApi.Data;

namespace SimpleBlogApi.Controllers
{

  [ApiController]
  [Route("users")]
  public class UsersController(AppDbContext context) : ControllerBase
  {

    private readonly AppDbContext _context = context;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      var users = await _context.Users.ToListAsync();

      return Ok(users);
    }
  }
}