using PedidosAPI.DTOs;

namespace PedidosAPI.Services.Interfaces;

public interface IClienteService
{
    Task<PageResponse<ClienteResponse>> ListarPaginadoAsync(int page, int pageSize, string? nome, char? tipoPessoa, bool ativo = true);
    Task<ClienteResponse> ObterPorIdAsync(long clienteId);
    Task<ClienteResponse> CadastrarAsync(ClienteCreateRequest clienteCreateRequest);
    Task<ClienteResponse> AtualizarAsync(long clienteId, ClienteUpdateRequest clienteUpdateRequest);
    Task RemoverAsync(long clienteId);
}