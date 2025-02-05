using EncantoApadrinhamento.Api.Configurations.DB;
using EncantoApadrinhamento.Infra.Interfaces;
using EncantoApadrinhamento.Infra.Interfaces.Base;
using EncantoApadrinhamento.Infra.Repositories;
using EncantoApadrinhamento.Infra.Repositories.Base;
using EncantoApadrinhamento.Services.Interfaces;
using EncantoApadrinhamento.Services.Services;

namespace EncantoApadrinhamento.Api.Configurations.DependencyInjection
{
    public static class DependencyInjectionConfiguration
    {
        public static void UseDependencyInjectionConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<DataBaseSeeder>();
        }
    }
}
