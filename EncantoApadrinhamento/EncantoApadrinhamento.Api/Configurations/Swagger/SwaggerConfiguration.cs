using Microsoft.OpenApi.Models;

namespace EncantoApadrinhamento.Api.Configurations.Swagger
{
    public static class SwaggerConfiguration
    {
        public static void UseSwaggerConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "EncantoApadrinhamento_Backend.API",
                    Version = "v1",
                    Description = "API de apadrinhamento",
                    Contact = new OpenApiContact
                    {
                        Name = "AppCobranca_Backend.API",
                        Email = "appsubspro@gmail.com",
                        Url = new System.Uri("https://www.linkedin.com/in/jordy-tonidandel-467257153/")
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Por favor, insira o token JWT no campo abaixo:",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        }, Array.Empty<string>()
                    }
                });
            });
        }
    }
}
