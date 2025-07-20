using System.ComponentModel.DataAnnotations;
using PedidosAPI.Models;

namespace PedidosAPI.DTOs;

public record ClienteEnderecoResponse
{
    public long Id { get; init; }

    public string Logradouro { get; init; } = default!;

    public string Numero { get; init; } = default!;

    public string? Complemento { get; init; }

    public string Bairro { get; init; } = default!;

    public MunicipioResponse Municipio { get; init; } = default!;

    public string Cep { get; init; } = default!;

    public bool PadraoEntrega { get; init; }

    public bool PadraoFaturamento { get; init; }

    public static ClienteEnderecoResponse FromEntity(ClienteEndereco entity)
    {
        return new ClienteEnderecoResponse
        {
            Id = entity.Id,
            Logradouro = entity.Logradouro,
            Numero = entity.Numero,
            Complemento = entity.Complemento,
            Bairro = entity.Bairro,
            Municipio = MunicipioResponse.FromEntity(entity.Municipio),
            Cep = entity.CodigoPostal,
            PadraoEntrega = entity.PadraoEntrega,
            PadraoFaturamento = entity.PadraoFaturamento
        };
    }

    public static IEnumerable<ClienteEnderecoResponse> ListFromEntityList(IEnumerable<ClienteEndereco> list) {
        return list.Select(FromEntity);
    }
}