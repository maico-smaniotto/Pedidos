using System.ComponentModel.DataAnnotations;

namespace PedidosAPI.DTOs;

public record ClienteUpdateRequest
{
    [Required(ErrorMessage = "Tipo de pessoa é obrigatório.")]
    public char TipoPessoa { get; init; }

    [Required(ErrorMessage = "Documento é obrigatório.")]
    public string DocPessoa { get; init; } = default!;

    [Required(ErrorMessage = "Razão social é obrigatória.")]
    public string RazaoSocial { get; init; } = default!;

    public string? NomeFantasia { get; init; }

    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string? Email { get; init; } = default!;

    public bool Ativo { get; init; }
}
