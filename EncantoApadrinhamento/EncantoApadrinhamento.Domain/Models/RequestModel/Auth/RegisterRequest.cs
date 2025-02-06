using EncantoApadrinhamento.Core.CustomAttibute;
using System.ComponentModel.DataAnnotations;

namespace EncantoApadrinhamento.Domain.Models.RequestModel.Auth
{
    public class RegisterRequest
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

    }
}
