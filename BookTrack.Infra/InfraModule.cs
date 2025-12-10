using System.Text;
using BookTrack.Core.Repositories;
using BookTrack.Core.Services;
using BookTrack.Infra.Persistence;
using BookTrack.Infra.Persistence.Repositories;
using BookTrack.Infra.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BookTrack.Infra;

public static class InfraModule
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration )
    {
        services
            .AddData(configuration)
            .AddAuth(configuration)
            .AddRepositories();
        
        return services;
    }

    private static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("BookTrackCs");

        services.AddDbContext<BookTrackDbContext>(o => o.UseSqlServer(connectionString));

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
            return services;
    }
    
    private static IServiceCollection AddAuth(this IServiceCollection services,  IConfiguration configuration)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });
            
        return services;
    }
}