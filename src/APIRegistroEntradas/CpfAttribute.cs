using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class CpfAttribute : AsyncValidationAttribute
{
    public CpfAttribute()
    {
        ErrorMessage = "Informe um CPF válido, com 11 dígitos ou no formato 000.000.000-00.";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null or "")
        {
            return ValidationResult.Success;
        }

        if (value is not string cpf || !CpfValido(cpf))
        {
            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        return ValidationResult.Success;
    }

    protected override Task<ValidationResult?> IsValidAsync(
        object? value, ValidationContext validationContext, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(IsValid(value, validationContext));
    }

    private static bool CpfValido(string cpf)
    {
        if (cpf.Length == 14 && cpf[3] == '.' && cpf[7] == '.' && cpf[11] == '-')
        {
            cpf = cpf.Remove(11, 1).Remove(7, 1).Remove(3, 1);
        }

        if (cpf.Length != 11 || cpf.Any(c => c is < '0' or > '9') || cpf.All(c => c == cpf[0]))
        {
            return false;
        }

        for (var digito = 9; digito < 11; digito++)
        {
            var soma = 0;
            for (var i = 0; i < digito; i++)
            {
                soma += (cpf[i] - '0') * (digito + 1 - i);
            }

            var resto = soma % 11;
            var esperado = resto < 2 ? 0 : 11 - resto;
            if (cpf[digito] - '0' != esperado)
            {
                return false;
            }
        }

        return true;
    }
}
