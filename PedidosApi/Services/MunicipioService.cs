using PedidosAPI.DTOs;
using PedidosAPI.Enums;
using PedidosAPI.Enums.Converters;
using PedidosAPI.Models;
using PedidosAPI.Repositories.Interfaces;
using PedidosAPI.Services.Interfaces;

namespace PedidosAPI.Services;

public class MunicipioService : IMunicipioService
{
    private readonly IMunicipioRepository _municipioRepository;

    public MunicipioService(IMunicipioRepository municipioRepository) {
        _municipioRepository = municipioRepository;
    }

    public async Task<PageResponse<MunicipioResponse>> ListarPaginadoAsync(int page, int pageSize, string? uf, string? nome)
    {
        UnidadeFederativa? unidadeFederativa = null;
        if (!string.IsNullOrWhiteSpace(uf))
        {
            unidadeFederativa = UnidadeFederativaConverter.FromValor(uf.ToUpper());
        }
        
        var content = await _municipioRepository.ObterPaginadoAsync(page, pageSize, unidadeFederativa, nome);
        var totalElements = await _municipioRepository.ContarTodosAsync(unidadeFederativa, nome);

        return PageResponse<MunicipioResponse>.Create(
            MunicipioResponse.ListFromEntityList(content),
            page,
            pageSize,
            totalElements
        );
    }
}