using EncantoApadrinhamento.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EncantoApadrinhamento.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(UserEntity user, IList<string> roles);
    }
}
