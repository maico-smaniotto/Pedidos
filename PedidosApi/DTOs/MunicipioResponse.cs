using PedidosAPI.Enums.Converters;
using PedidosAPI.Models;

namespace PedidosAPI.DTOs;

public record MunicipioResponse(
    string CodigoIbge,
    string Nome,
    String Uf
)
{
    public static MunicipioResponse FromEntity(Municipio entity)
    {
        return new MunicipioResponse(
            entity.CodigoIbge,
            entity.Nome,
            UnidadeFederativaConverter.GetValor(entity.Uf)
        );
    }

    public static IEnumerable<MunicipioResponse> ListFromEntityList(IEnumerable<Municipio> list)
    {
        return list.Select(FromEntity);
    }
}