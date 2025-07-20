using PedidosAPI.DTOs;

namespace PedidosAPI.Services.Interfaces;

public interface IClienteEnderecoService
{
    Task<PageResponse<ClienteEnderecoResponse>> ListarPaginadoAsync(int page, int pageSize, long clienteId);
    Task<ClienteEnderecoResponse> ObterPorIdAsync(long clienteEnderecoId);
    Task<ClienteEnderecoResponse> CadastrarAsync(long clienteId, ClienteEnderecoRequest clienteEnderecoRequest);
    Task<ClienteEnderecoResponse> AtualizarAsync(long clienteEnderecoId, ClienteEnderecoRequest clienteEnderecoRequest);
    Task RemoverAsync(long clienteEnderecoId);
}