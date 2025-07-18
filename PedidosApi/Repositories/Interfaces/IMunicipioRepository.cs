using PedidosAPI.Enums;
using PedidosAPI.Models;

namespace PedidosAPI.Repositories.Interfaces;

public interface IMunicipioRepository
{
    Task<IEnumerable<Municipio>> ObterTodosAsync(UnidadeFederativa? uf, string? nome);
    Task<IEnumerable<Municipio>> ObterPaginadoAsync(int page, int pageSize, UnidadeFederativa? uf, string? nome);
    Task<Municipio?> ObterPorIdAsync(string id);
    Task<Municipio> AdicionarAsync(Municipio municipio);
    Task<Municipio> AtualizarAsync(Municipio municipio);
    Task RemoverAsync(string id);
    Task<bool> ExisteAsync(string id);
    Task<int> ContarTodosAsync(UnidadeFederativa? uf, string? nome);
}