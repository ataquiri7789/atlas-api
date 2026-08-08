namespace AtlasCommerce.Application.Features.Autentication.Commands.Login;

public class LoginResponse
{
    public string AccessToken { get; set; } = string.Empty;

    public DateTime Expiration { get; set; }
}