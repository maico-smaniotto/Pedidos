using PedidosAPI.Enums.Converters;
using PedidosAPI.Models;

namespace PedidosAPI.DTOs;

public record ClienteResponse
(
    long Id,
    char TipoPessoa,
    string TipoPessoaDescricao,
    string DocPessoa,
    string RazaoSocial,
    string NomeFantasia,
    string Email,
    bool Ativo
)
{
    public static ClienteResponse FromEntity(Cliente entity)
    {
        return new ClienteResponse(
            entity.Id,
            TipoPessoaConverter.GetValor(entity.TipoPessoa),
            TipoPessoaConverter.GetDescricao(entity.TipoPessoa),
            entity.CpfCnpj,
            entity.RazaoSocial,
            entity.NomeFantasia,
            entity.Email ?? "",
            entity.StatusRegistro == Enums.StatusRegistro.Ativo
        );
    }

    public static IEnumerable<ClienteResponse> ListFromEntityList(IEnumerable<Cliente> list)
    {
        return list.Select(FromEntity);
    }
}