using EncantoApadrinhamento.Api.Configurations.Cors;
using EncantoApadrinhamento.Api.Configurations.DBConfig;
using EncantoApadrinhamento.Api.Configurations.DependencyInjection;
using EncantoApadrinhamento.Api.Configurations.ExceptionHandler;
using EncantoApadrinhamento.Api.Configurations.Identity;
using EncantoApadrinhamento.Api.Configurations.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.UseIdentityConfiguration();
builder.UseDBConfiguration();
builder.UseCorsConfiguration();

builder.UseDependencyInjectionConfiguration();
builder.Services.AddEndpointsApiExplorer();
builder.UseSwaggerConfiguration();

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
