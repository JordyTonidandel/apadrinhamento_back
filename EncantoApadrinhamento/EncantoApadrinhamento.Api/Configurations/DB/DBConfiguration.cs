using EncantoApadrinhamento.Api.Configurations.DB;
using EncantoApadrinhamento.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace EncantoApadrinhamento.Api.Configurations.DBConfig
{
    public static class DBConfiguration
    {
        public static void UseDBConfiguration(this WebApplicationBuilder builder)
        {
            if (builder.Environment.IsProduction())
            {
                builder.Services.AddDbContext<AppDBContext>(options =>
                {
                    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
                });
            }
            else
            {
                builder.Services.AddDbContext<AppDBContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                });
            }
        }

        public async static void UseDBSeederConfiguration(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;
            try
            {
                var seeder = services.GetRequiredService<DataBaseSeeder>();
                await seeder.UseSeedAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao realizar o seed do banco: {ex.Message}");
            }
        }
    }
}
