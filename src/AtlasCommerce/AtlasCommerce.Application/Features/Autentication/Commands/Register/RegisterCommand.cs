using AtlasCommerce.Application.Common.Results;
using MediatR;

namespace AtlasCommerce.Application.Features.Authentication.Commands.Register;

public class RegisterCommand : IRequest<Result<RegisterResponse>>
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Role { get; set; } = "User";
}