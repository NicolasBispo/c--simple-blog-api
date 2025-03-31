using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SimpleBlogApi.Data;
using SimpleBlogApi.Dto.User;
using SimpleBlogApi.Models;

public class AuthService
{
  private readonly AppDbContext _context;
  private readonly IConfiguration _config;
  public AuthService(AppDbContext context, IConfiguration configuration)
  {
    _context = context;
    _config = configuration;
  }

  public async Task<string?> AuthenticateUser(string email, string password)
  {
    var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == email);
    if (user == null || VerifyPassword(password, user.Password))
    {
      return null;
    }
    return GenerateToken(user);

  }

  public async Task<User?> CreateUser(UserRegisterDto userRegisterDto)
  {

    if (await _context.Users.AnyAsync(x => x.Email == userRegisterDto.Email))
    {
      throw new InvalidOperationException("Email address already in use");
    }

    var user = new User
    {
      Email = userRegisterDto.Email,
      Password = HashPassword(userRegisterDto.Password)
    };
    _context.Users.Add(user);
    await _context.SaveChangesAsync();
    return user;

  }
  private string GenerateToken(User user)
  {
    var jwtKey = _config["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is not configured.");
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var claims = new[]
    {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim("userId", user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

    var token = new JwtSecurityToken(
        issuer: _config["Jwt:Issuer"],
        audience: _config["Jwt:Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddHours(1),
        signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }

  public static string HashPassword(string password)
  {
    var bytes = Encoding.UTF8.GetBytes(password);
    var hash = SHA256.HashData(bytes);
    return Convert.ToBase64String(hash);
  }
  private static bool VerifyPassword(string password, string hash)
  {
    return HashPassword(password) == hash;
  }
}