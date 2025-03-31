using Microsoft.AspNetCore.Mvc;
using SimpleBlogApi.Dto.User;

namespace SimpleBlogApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AuthController : ControllerBase
  {
    private readonly AuthService _authService;
    public AuthController(AuthService authService)
    {
      _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto login)
    {
      var token = await _authService.AuthenticateUser(login.Email, login.Password);
      if (token == null)
      {
        return Unauthorized(new { Message = "Invalid login credentials" });
      }

      Response.Headers.Append("Authorization", $"Bearer {token}");
      return Ok();
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto register)
    {
      try
      {
        var user = await _authService.CreateUser(register);
        return Ok(new { Message = "User registered with success", User = user });
      }
      catch (InvalidOperationException ex)
      {
        return BadRequest(ex.Message);
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { Message = ex.Message, Error = ex });
      }

    }
  }
}