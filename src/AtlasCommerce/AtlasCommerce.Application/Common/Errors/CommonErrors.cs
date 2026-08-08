namespace AtlasCommerce.Application.Common.Errors;

public static class CommonErrors
{
    public static readonly Error Unexpected =
        Error.Unexpected(
            $"{nameof(CommonErrors)}.{nameof(Unexpected)}",
            "An unexpected error has occurred.");

    public static readonly Error Unauthorized =
        Error.Unauthorized(
            $"{nameof(CommonErrors)}.{nameof(Unauthorized)}",
            "Unauthorized access.");

    public static readonly Error Forbidden =
        Error.Forbidden(
            $"{nameof(CommonErrors)}.{nameof(Forbidden)}",
            "Access denied.");

    public static readonly Error Validation =
        Error.Validation(
            $"{nameof(CommonErrors)}.{nameof(Validation)}",
            "One or more validation errors occurred.");
}