namespace _002.AuthenticationWithMvc.Dtos;

public class LoginViewModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string? ReturnUrl { get; set; }
}