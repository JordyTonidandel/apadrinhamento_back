using EncantoApadrinhamento.Api.Configurations.Authentication;
using EncantoApadrinhamento.Api.Configurations.Authorization;
using EncantoApadrinhamento.Api.Configurations.Cors;
using EncantoApadrinhamento.Api.Configurations.DBConfig;
using EncantoApadrinhamento.Api.Configurations.DependencyInjection;
using EncantoApadrinhamento.Api.Configurations.ExceptionHandler;
using EncantoApadrinhamento.Api.Configurations.Identity;
using EncantoApadrinhamento.Api.Configurations.Mappings;
using EncantoApadrinhamento.Api.Configurations.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.UseIdentityConfiguration();
builder.UseDBConfiguration();
builder.UseCorsConfiguration();
builder.UseAuthenticationConfiguration();
builder.UseAuthorizationConfiguration();

builder.UseDependencyInjectionConfiguration();
builder.UseMapsterConfiguration();
builder.UseSwaggerConfiguration();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDBSeederConfiguration();

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("CorsPolicy");

app.MapControllers();
app.UseExceptionHandlerConfiguration();

app.Run();
