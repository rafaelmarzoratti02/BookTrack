using BookTrack.Application.Services;
using BookTrack.Core.Entitites;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookTrack.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddServices()
            .AddValidators();
        
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation().AddValidatorsFromAssemblyContaining<BookService>();
        return services;
    }
}