using System.ComponentModel.DataAnnotations;

public sealed class CheckInRequest
{
    [Required(ErrorMessage = "O CPF é obrigatório.")]
    [Cpf]
    public string? Cpf { get; init; }

    [Required(ErrorMessage = "A data e o horário são obrigatórios.")]
    public DateTimeOffset? DataHora { get; init; }

    [Required(ErrorMessage = "O local é obrigatório.")]
    public string? Local { get; init; }
}
