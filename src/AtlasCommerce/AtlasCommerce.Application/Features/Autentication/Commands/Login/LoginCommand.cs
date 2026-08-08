using AtlasCommerce.Application.Common.Results;
using AtlasCommerce.Application.Features.Autentication.Commands.Login;
using AtlasCommerce.Application.Features.Authentication.DTOs;
using MediatR;

namespace AtlasCommerce.Application.Features.Authentication.Commands.Login;

public class LoginCommand : IRequest<Result<LoginResponse>>
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}