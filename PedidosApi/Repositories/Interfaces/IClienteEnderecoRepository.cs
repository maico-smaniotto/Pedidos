using PedidosAPI.Models;

namespace PedidosAPI.Repositories.Interfaces;

public interface IClienteEnderecoRepository
{
    Task<IEnumerable<ClienteEndereco>> ObterPaginadoAsync(int page, int pageSize, long clienteId, bool ativo = true);
    Task<ClienteEndereco?> ObterPorIdAsync(long id);
    Task<ClienteEndereco> AdicionarAsync(ClienteEndereco clienteEndereco);
    Task<ClienteEndereco> AtualizarAsync(ClienteEndereco clienteEndereco);
    Task RemoverAsync(long id);
    Task<bool> ExisteAsync(long id);
    Task<int> ContarTodosAsync(long clienteId, bool ativo = true);
}