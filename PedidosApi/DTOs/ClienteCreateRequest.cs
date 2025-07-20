using System.ComponentModel.DataAnnotations;

namespace PedidosAPI.DTOs;

public record ClienteCreateRequest
{
    [Required(ErrorMessage = "Tipo de pessoa é obrigatório.")]
    public char TipoPessoa { get; init; }

    [Required(ErrorMessage = "Documento é obrigatório.")]
    public string DocPessoa { get; init; } = default!;

    [Required(ErrorMessage = "Razão social é obrigatória.")]
    public string RazaoSocial { get; init; } = default!;

    public string? NomeFantasia { get; init; }

    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string? Email { get; init; }

    public bool Ativo { get; init; }

    [Required(ErrorMessage = "Ao menos um endereço deve ser informado.")]
    [MinLength(1, ErrorMessage = "Ao menos um endereço deve ser informado.")]
    public IEnumerable<ClienteEnderecoRequest> Enderecos { get; init; } = default!;
}
