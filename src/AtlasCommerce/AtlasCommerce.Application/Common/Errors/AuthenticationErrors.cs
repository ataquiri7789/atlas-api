namespace AtlasCommerce.Application.Common.Errors;

public static class AuthenticationErrors
{
    public static readonly Error InvalidCredentials =
        Error.Unauthorized(
            $"{nameof(AuthenticationErrors)}.{nameof(InvalidCredentials)}",
            "Invalid email or password.");

    public static readonly Error UserNotFound =
        Error.NotFound(
            $"{nameof(AuthenticationErrors)}.{nameof(UserNotFound)}",
            "User not found.");

    public static readonly Error UserAlreadyExists =
        Error.Conflict(
            $"{nameof(AuthenticationErrors)}.{nameof(UserAlreadyExists)}",
            "The email is already registered.");

    public static readonly Error InvalidToken =
        Error.Unauthorized(
            $"{nameof(AuthenticationErrors)}.{nameof(InvalidToken)}",
            "Invalid token.");

    public static readonly Error ExpiredToken =
        Error.Unauthorized(
            $"{nameof(AuthenticationErrors)}.{nameof(ExpiredToken)}",
            "The token has expired.");
}
