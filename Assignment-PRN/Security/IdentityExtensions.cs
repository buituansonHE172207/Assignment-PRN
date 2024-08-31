using Assignment_PRN.Data;
using Assignment_PRN.Entities;
using Microsoft.AspNetCore.Identity;

namespace Assignment_PRN.Security
{
    public static class IdentityExtensions
    {
        public static void AddIdentitySetup(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultIdentity<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.User.RequireUniqueEmail = true;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AssignmentPRNContext>();

            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            });
        }
    }
}

