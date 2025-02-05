using Mapster;
using MapsterMapper;
using System.Reflection;

namespace EncantoApadrinhamento.Api.Configurations.Mappings
{
    public static class MapsterConfiguration
    {
        public static void UseMapsterConfiguration(this WebApplicationBuilder builder)
        {
            var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
            typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());
            var mapperConfig = new Mapper(typeAdapterConfig);
            builder.Services.AddSingleton<IMapper>(mapperConfig);
        }

    }
}
