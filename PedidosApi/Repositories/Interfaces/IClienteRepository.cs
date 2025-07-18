using PedidosAPI.Enums;
using PedidosAPI.Models;

namespace PedidosAPI.Repositories.Interfaces;

public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> ObterPaginadoAsync(int page, int pageSize, string? nome, TipoPessoa? tipoPessoa, bool ativo = true);
    Task<Cliente?> ObterPorIdAsync(long id);
    Task<Cliente> AdicionarAsync(Cliente cliente);
    Task<Cliente> AtualizarAsync(Cliente cliente);
    Task RemoverAsync(Cliente cliente);
    Task<bool> ExisteAsync(long id);
    Task<int> ContarTodosAsync(string? nome, TipoPessoa? tipoPessoa, bool ativo = true);
}