using System.ComponentModel.DataAnnotations;

namespace PedidosAPI.DTOs;

public record ClienteEnderecoRequest
{
    [Required(ErrorMessage = "Logradouro é obrigatório.")]
    public string Logradouro { get; init; } = default!;

    [Required(ErrorMessage = "Número do endereço é obrigatório.")]
    public string Numero { get; init; } = default!;

    public string? Complemento { get; init; }

    [Required(ErrorMessage = "Bairro é obrigatório.")]
    public string Bairro { get; init; } = default!;

    [Required(ErrorMessage = "Código IBGE do município é obrigatório.")]
    public string MunicipioCodigoIbge { get; init; } = default!;

    [Required(ErrorMessage = "CEP é obrigatório.")]
    public string Cep { get; init; } = default!;

    public bool PadraoEntrega { get; init; }
    
    public bool PadraoFaturamento { get; init; }
}