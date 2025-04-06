using Application.Interfaces.Services.Admins;
using Application.Interfaces.Services.Books;
using Application.Interfaces.Services.Members;
using Application.Interfaces.Services.Notifications;
using Application.Interfaces.Services.Users;
using Application.Interfaces.Services.Pages;
using Application.Interfaces.Services.Produits;
using Application.Services.Admins;
using Application.Services.Books;
using Application.Services.Members;
using Application.Services.Notifications;
using Application.Services.Users;
using Application.Services.Pages;
using Application.Services.Produits;
using Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Slugify;

namespace Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<ApplicationSettings>(configuration.GetSection("Application"));
        services.Configure<JwtTokenSettings>(configuration.GetSection("JwtToken"));
        services.Configure<CookieSettings>(configuration.GetSection("Cookies"));

        services.AddScoped<ISlugHelper, SlugHelper>();

        services.AddScoped<IAuthenticatedAdminService, AuthenticatedAdminService>();
        services.AddScoped<IBookCreationService, BookCreationService>();
        services.AddScoped<IBookUpdateService, BookUpdateService>();
        
        services.AddScoped<IProduitCreationService, ProduitCreationService>();
        
        services.AddScoped<IUserCreationService, UserCreationService>();
        services.AddScoped<IMemberRegistrationService, MemberRegistrationService>();
        services.AddScoped<INotificationService, EmailNotificationService>();

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
        services.AddScoped<IAuthenticatedMemberService, AuthenticatedMemberService>();
        
        services.AddScoped<IPageCreationService, PageCreationService>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}