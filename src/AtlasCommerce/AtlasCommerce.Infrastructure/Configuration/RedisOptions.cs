namespace AtlasCommerce.Infrastructure.Configuration;

public class RedisOptions
{
    public string Connection { get; set; } = string.Empty;

    public int DefaultExpirationMinutes { get; set; }
}