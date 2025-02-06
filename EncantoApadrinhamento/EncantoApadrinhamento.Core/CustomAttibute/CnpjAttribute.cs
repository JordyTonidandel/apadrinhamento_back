using System.ComponentModel.DataAnnotations;

namespace EncantoApadrinhamento.Core.CustomAttibute
{
    public class CnpjAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var cnpj = value as string;

            if (string.IsNullOrWhiteSpace(cnpj))
            {
                return new ValidationResult("O CNPJ é obrigatório.");
            }

            cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

            if (cnpj.Length != 14)
            {
                return new ValidationResult("O CNPJ deve conter 14 dígitos.");
            }

            if (cnpj.Distinct().Count() == 1)
            {
                return new ValidationResult("O CNPJ informado é inválido.");
            }

            if (!IsCnpjValid(cnpj))
            {
                return new ValidationResult("O CNPJ informado é inválido.");
            }

            return ValidationResult.Success!;
        }

        private bool IsCnpjValid(string cnpj)
        {
            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            }
            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            tempCnpj += digito1;
            soma = 0;
            for (int i = 0; i < 13; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            }
            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            return cnpj.EndsWith($"{digito1}{digito2}");
        }
    }
}
