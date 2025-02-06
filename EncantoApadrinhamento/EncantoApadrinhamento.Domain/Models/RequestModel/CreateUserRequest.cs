using EncantoApadrinhamento.Core.CustomAttibute;
using System.ComponentModel.DataAnnotations;
using static EncantoApadrinhamento.Core.Enums.Enums;

namespace EncantoApadrinhamento.Domain.RequestModel
{
    public class CreateUserRequest
    {
        [MinLength(3)]
        [MaxLength(100)]
        public required string Name { get; set; }

        [MinLength(3)]
        [MaxLength(100)]
        public required string LastName { get; set; }

        [PasswordComplexity]
        public required string Password { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        [Cpf]
        public required string Cpf { get; set; }

        public Role UserRole { get; set; }

        public required DateTime BirthDate { get; set; }

        public required string PhoneNumber { get; set; }
    }
}
