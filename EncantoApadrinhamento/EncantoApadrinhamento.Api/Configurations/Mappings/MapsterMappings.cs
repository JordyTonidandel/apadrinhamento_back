using EncantoApadrinhamento.Domain.Entities;
using EncantoApadrinhamento.Domain.ResponseModel;
using Mapster;

namespace EncantoApadrinhamento.Api.Configurations.Mappings
{
    public class MapsterMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UserEntity, UserResponse>()
                .Map(dest => dest.FullName, src => $"{src.Name} {src.LastName}");
        }

    }
}
