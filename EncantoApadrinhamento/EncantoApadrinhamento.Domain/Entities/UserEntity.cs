using Microsoft.AspNetCore.Identity;

namespace EncantoApadrinhamento.Domain.Entities
{
    public class UserEntity : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }

        public virtual AddressEntity? Address { get; set; } = new AddressEntity();
    }
}
