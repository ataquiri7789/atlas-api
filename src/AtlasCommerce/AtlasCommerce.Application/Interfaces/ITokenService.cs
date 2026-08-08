using AtlasCommerce.Application.Features.Authentication.DTOs;

namespace AtlasCommerce.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(TokenRequest request);
}