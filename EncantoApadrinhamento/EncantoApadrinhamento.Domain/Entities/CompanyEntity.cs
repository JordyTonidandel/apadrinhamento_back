using EncantoApadrinhamento.Domain.Enums;

namespace EncantoApadrinhamento.Domain.Entities
{
    public class CompanyEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string SocialName { get; set; } = string.Empty;
        public string FantasyName { get; set; } = string.Empty;
        public CompanyTypeEnum CompanyType { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; }  = string.Empty;
        public RegistrationStatusEnum RegistrationStatus { get; set; }

        public string? AddressId { get; set; } = string.Empty;
        public virtual AddressEntity? Address { get; set; }

        public string? UserId { get; set; } = string.Empty;
        public virtual UserEntity? User { get; set; }
    }
}
