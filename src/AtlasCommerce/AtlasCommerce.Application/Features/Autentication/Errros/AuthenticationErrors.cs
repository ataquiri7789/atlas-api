using AtlasCommerce.Application.Common.Errors;

namespace AtlasCommerce.Application.Features.Authentication.Errors;

public static class AuthenticationErrors
{
    public static Error InvalidCredentials =>
        Error.Failure(
            "AUTH.INVALID_CREDENTIALS",
            "Correo o contraseña incorrectos",
            ErrorType.Business,
            "Authentication");

    public static Error UserAlreadyExists =>
        Error.Failure(
            "AUTH.USER_ALREADY_EXISTS",
            "El usuario ya existe",
            ErrorType.Conflict,
            "Authentication");
}