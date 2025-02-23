﻿using System.ComponentModel.DataAnnotations;

namespace EncantoApadrinhamento.Core.CustomAttibute
{
    public class CpfAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var cpf = value as string;

            if (string.IsNullOrWhiteSpace(cpf))
            {
                return new ValidationResult("O CPF é obrigatório.");
            }

            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
            {
                return new ValidationResult("O CPF deve conter 11 dígitos.");
            }

            if (cpf.Distinct().Count() == 1)
            {
                return new ValidationResult("O CPF informado é inválido.");
            }

            if (!IsCpfValid(cpf))
            {
                return new ValidationResult("O CPF informado é inválido.");
            }

            return ValidationResult.Success!;
        }

        private bool IsCpfValid(string cpf)
        {
            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }
            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            tempCpf += digito1;
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }
            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            return cpf.EndsWith($"{digito1}{digito2}");
        }
    }
}
