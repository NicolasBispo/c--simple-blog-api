namespace SimpleBlogApi.Dto.User
{
  public class UserRegisterDto
  {
    public required string Email { get; set; }
    public required string Password {get; set;}
    public required string PasswordConfirmation { get; set; }
  }
}