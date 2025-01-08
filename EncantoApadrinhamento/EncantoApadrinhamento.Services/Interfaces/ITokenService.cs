using Microsoft.AspNetCore.Identity;

namespace EncantoApadrinhamento.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(IdentityUser user, IList<string> roles);
    }
}
