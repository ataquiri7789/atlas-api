using AtlasCommerce.Application.Common.Errors;
using AtlasCommerce.Application.Common.Results;
using AtlasCommerce.Application.Features.Authentication.Commands.Login;
using AtlasCommerce.Application.Interfaces;
using AtlasCommerce.Domain.Entities;
using AtlasCommerce.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AtlasCommerce.Application.Features.Authentication.Commands.Register;

public class RegisterCommandHandler
    : IRequestHandler<RegisterCommand, Result<RegisterResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<LoginCommandHandler> _logger;


    public RegisterCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork,
        ILogger<LoginCommandHandler> logger)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<RegisterResponse>> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        var existingUser =
            await _userRepository.GetByEmailAsync(request.Email);

        if (existingUser != null)
        {
            _logger.LogInformation("Validando usuario {Email}", request.Email);
            return Result<RegisterResponse>.Failure(AuthenticationErrors.UserAlreadyExists);
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = _passwordHasher.Hash(request.Password),
            Role = request.Role,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        await _userRepository.AddAsync(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new RegisterResponse
        {
            UserId = user.Id,
            Message = "Usuario registrado correctamente."
        };
    }
}