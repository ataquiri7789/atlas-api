using AtlasCommerce.Application.Interfaces;
using AtlasCommerce.Domain.Repositories;
using AtlasCommerce.Infrastructure.Cache.Redis;
using AtlasCommerce.Infrastructure.Configuration;
using AtlasCommerce.Infrastructure.Persistence.Context;
using AtlasCommerce.Infrastructure.Persistence.Repositories;
using AtlasCommerce.Infrastructure.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using AtlasCommerce.Infrastructure.Security.JwtAuthentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AtlasCommerce.Infrastructure.Security.Cryptography;
using System.Text;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AtlasCommerceDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Redis
        services.Configure<RedisOptions>(configuration.GetSection("Redis"));

        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var options = sp
                .GetRequiredService<IOptions<RedisOptions>>()
                .Value;

            return ConnectionMultiplexer.Connect(options.Connection);
        });


        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            var jwt = configuration
                .GetSection("Jwt")
                .Get<JwtOptions>()!;

            options.TokenValidationParameters =
                new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwt.Issuer,

                    ValidAudience = jwt.Audience,

                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwt.Key))
                };
        });


        services.AddScoped<ICacheService, RedisCacheService>();
        services.AddScoped<ITokenService, JwtTokenService>();
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
        return services;
    }
}