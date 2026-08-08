using AtlasCommerce.Application.Features.Autentication.Commands.Login;
using AtlasCommerce.Application.Features.Authentication.DTOs;
using AtlasCommerce.Application.Interfaces;
using AtlasCommerce.Domain.Repositories;
using MediatR;


namespace AtlasCommerce.Application.Features.Authentication.Commands.Login;

using AtlasCommerce.Application.Common.Errors;
using AtlasCommerce.Application.Common.Results;
using Microsoft.Extensions.Logging;

public class LoginCommandHandler
    : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly ILogger<LoginCommandHandler> _logger;
    public LoginCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenService tokenService,
        ILogger<LoginCommandHandler> logger)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _logger = logger;
    }

    public async Task<Result<LoginResponse>> Handle(LoginCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository
            .GetByEmailAsync(request.Email);

        if (user is null)
        {
            _logger.LogInformation("Validando usuario {Email}", request.Email);
            return Result<LoginResponse>.Failure(
                AuthenticationErrors.InvalidCredentials);
        }

        var isValidPassword =_passwordHasher.Verify(request.Password,user.PasswordHash);

        if (!isValidPassword)
        {
            return Result<LoginResponse>.Failure(AuthenticationErrors.InvalidCredentials);
        }

        var token = _tokenService.GenerateToken(
            new TokenRequest
            {
                UserId = user.Id,
                Email = user.Email,
                Role = user.Role
            });

        return new LoginResponse
        {
            AccessToken = token,
            Expiration = DateTime.UtcNow.AddHours(1)
        };
    }
}