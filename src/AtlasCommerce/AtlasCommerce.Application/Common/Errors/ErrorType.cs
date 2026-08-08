public enum ErrorType
{
    None = 0,          // 🔥 NECESARIO para Result
    Validation = 1,
    Business = 2,
    NotFound = 3,
    Conflict = 4,
    Unauthorized = 5,
    Forbidden = 6,
    Infrastructure = 7,
    Technical = 8,
    Unexpected = 9     // 🔥 para errores no controlados
}