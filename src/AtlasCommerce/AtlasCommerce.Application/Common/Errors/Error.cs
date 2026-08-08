using AtlasCommerce.Application.Common.Errors;

public sealed class Error
{
    public string Code { get; }
    public string Description { get; }
    public ErrorType Type { get; }
    public string Module { get; }

    private Error(
        string code,
        string description,
        ErrorType type,
        string module)
    {
        Code = code;
        Description = description;
        Type = type;
        Module = module;
    }

    // 🔥 BASE
    public static Error Failure(
        string code,
        string description,
        ErrorType type,
        string module)
        => new(code, description, type, module);

    // 🔥 TIPOS
    public static Error Validation(
        string code,
        string description,
        string module = "Application")
        => new(code, description, ErrorType.Validation, module);

    public static Error NotFound(
        string code,
        string description,
        string module = "Application")
        => new(code, description, ErrorType.NotFound, module);

    public static Error Conflict(
        string code,
        string description,
        string module = "Application")
        => new(code, description, ErrorType.Conflict, module);

    public static Error Unauthorized(
        string code,
        string description,
        string module = "Application")
        => new(code, description, ErrorType.Unauthorized, module);

    public static Error Forbidden(
        string code,
        string description,
        string module = "Application")
        => new(code, description, ErrorType.Forbidden, module);

    public static Error Unexpected(
        string code,
        string description,
        string module = "System")
        => new(code, description, ErrorType.Unexpected, module);

    // 🔥 IMPORTANTE (para Result)
    public static readonly Error None =
        new(string.Empty, string.Empty, ErrorType.None, string.Empty);
}