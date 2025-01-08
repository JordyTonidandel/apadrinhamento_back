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
    }
}
