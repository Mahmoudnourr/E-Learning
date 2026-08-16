using Infrastructure.Identity;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
    {
     services
     .AddIdentityCore<ApplicationUser>(options =>
     {
         options.User.RequireUniqueEmail = true;
         options.Password.RequiredLength = 8;
         options.Password.RequireDigit = true;
         options.Password.RequireUppercase = true;
         options.Password.RequireLowercase = true;
     })
     .AddRoles<IdentityRole<Guid>>()
     .AddEntityFrameworkStores<ApplicationDbContext>()
     .AddSignInManager()
     .AddDefaultTokenProviders();

        return services;
    }
}
