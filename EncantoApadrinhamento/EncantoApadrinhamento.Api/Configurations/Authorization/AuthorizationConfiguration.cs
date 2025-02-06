namespace EncantoApadrinhamento.Api.Configurations.Authorization
{
    public static class AuthorizationConfiguration
    {
        public static void UseAuthorizationConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Owner", policy => policy.RequireRole("Owner"));
                options.AddPolicy("Administrator", policy => policy.RequireRole("Administrator"));
                options.AddPolicy("Company", policy => policy.RequireRole("Company"));
                options.AddPolicy("User", policy => policy.RequireRole("User"));
            });
        }
    }
}
