namespace AtlasCommerce.Application.Features.Authentication.DTOs;

public class TokenRequest
{
    public Guid UserId { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
}