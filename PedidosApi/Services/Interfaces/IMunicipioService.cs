using PedidosAPI.DTOs;

namespace PedidosAPI.Services.Interfaces;

public interface IMunicipioService
{
    Task<PageResponse<MunicipioResponse>> ListarPaginadoAsync(int page, int pageSize, string? uf, string? nome);
}