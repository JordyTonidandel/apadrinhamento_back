using EncantoApadrinhamento.Domain.Entities;
using EncantoApadrinhamento.Infra.Context;
using Microsoft.AspNetCore.Identity;

namespace EncantoApadrinhamento.Api.Configurations.Identity
{
    public static class IdentityConfiguration
    {
        public static void UseIdentityConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<UserEntity, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.User.RequireUniqueEmail = true;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            })
                .AddEntityFrameworkStores<AppDBContext>()
                .AddDefaultTokenProviders();
        }
    }
}
