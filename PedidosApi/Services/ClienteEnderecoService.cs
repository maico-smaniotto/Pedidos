using PedidosAPI.DTOs;
using PedidosAPI.Enums;
using PedidosAPI.Exceptions;
using PedidosAPI.Models;
using PedidosAPI.Repositories.Interfaces;
using PedidosAPI.Services.Interfaces;

public class ClienteEnderecoService : IClienteEnderecoService
{
    private readonly IClienteEnderecoRepository _clienteEnderecoRepository;
    private readonly IMunicipioRepository _municipioRepository;
    private readonly IClienteRepository _clienteRepository;

    public ClienteEnderecoService(IClienteEnderecoRepository clienteEnderecoRepository, IMunicipioRepository municipioRepository, IClienteRepository clienteRepository)
    {
        _clienteEnderecoRepository = clienteEnderecoRepository;
        _municipioRepository = municipioRepository;
        _clienteRepository = clienteRepository;
    }

    public async Task<PageResponse<ClienteEnderecoResponse>> ListarPaginadoAsync(int page, int pageSize, long clienteId)
    {
        var content = await _clienteEnderecoRepository.ObterPaginadoAsync(page, pageSize, clienteId, true);
        var totalElements = await _clienteEnderecoRepository.ContarTodosAsync(clienteId, true);

        return PageResponse<ClienteEnderecoResponse>.Create(
            ClienteEnderecoResponse.ListFromEntityList(content),
            page,
            pageSize,
            totalElements
        );
    }

    public async Task<ClienteEnderecoResponse> ObterPorIdAsync(long clienteEnderecoId)
    {
        var content = await _clienteEnderecoRepository.ObterPorIdAsync(clienteEnderecoId) ?? throw new NotFoundException("Cliente > Endereço", clienteEnderecoId);
        return ClienteEnderecoResponse.FromEntity(content);
    }

    public async Task<ClienteEnderecoResponse> CadastrarAsync(long clienteId, ClienteEnderecoRequest clienteEnderecoRequest)
    {
        var cliente = await _clienteRepository.ObterPorIdAsync(clienteId) ?? throw new NotFoundException("Cliente", clienteId);
        var municipio = await _municipioRepository.ObterPorIdAsync(clienteEnderecoRequest.MunicipioCodigoIbge) ?? throw new NotFoundException("Município", clienteEnderecoRequest.MunicipioCodigoIbge);

        var clienteEndereco = new ClienteEndereco
        {
            Logradouro = clienteEnderecoRequest.Logradouro,
            Numero = clienteEnderecoRequest.Numero,
            Complemento = clienteEnderecoRequest.Complemento,
            Bairro = clienteEnderecoRequest.Bairro,
            MunicipioCodigoIbge = clienteEnderecoRequest.MunicipioCodigoIbge,
            Municipio = municipio,
            CodigoPostal = clienteEnderecoRequest.Cep,
            PadraoFaturamento = clienteEnderecoRequest.PadraoFaturamento,
            PadraoEntrega = clienteEnderecoRequest.PadraoEntrega,
            ClienteId = clienteId,
            Cliente = cliente,
            StatusRegistro = StatusRegistro.Ativo
        };

        var result = await _clienteEnderecoRepository.AdicionarAsync(clienteEndereco);
        return ClienteEnderecoResponse.FromEntity(result);
    }

    public async Task<ClienteEnderecoResponse> AtualizarAsync(long clienteEnderecoId, ClienteEnderecoRequest clienteEnderecoRequest)
    {
        var clienteEndereco = await _clienteEnderecoRepository.ObterPorIdAsync(clienteEnderecoId) ?? throw new NotFoundException("Cliente > Endereço", clienteEnderecoId);

        clienteEndereco.Logradouro = clienteEnderecoRequest.Logradouro;
        clienteEndereco.Numero = clienteEnderecoRequest.Numero;
        clienteEndereco.Complemento = clienteEnderecoRequest.Complemento;
        clienteEndereco.Bairro = clienteEnderecoRequest.Bairro;

        if (clienteEndereco.MunicipioCodigoIbge != clienteEnderecoRequest.MunicipioCodigoIbge)
        {
            var municipio = await _municipioRepository.ObterPorIdAsync(clienteEnderecoRequest.MunicipioCodigoIbge)
                ?? throw new NotFoundException("Município", clienteEnderecoRequest.MunicipioCodigoIbge);

            clienteEndereco.MunicipioCodigoIbge = clienteEnderecoRequest.MunicipioCodigoIbge;
            clienteEndereco.Municipio = municipio;
        }

        clienteEndereco.CodigoPostal = clienteEnderecoRequest.Cep;
        clienteEndereco.PadraoFaturamento = clienteEnderecoRequest.PadraoFaturamento;
        clienteEndereco.PadraoEntrega = clienteEnderecoRequest.PadraoEntrega;

        await _clienteEnderecoRepository.AtualizarAsync(clienteEndereco);
        return ClienteEnderecoResponse.FromEntity(clienteEndereco);
    }

    public async Task RemoverAsync(long clienteEnderecoId)
    {
        var clienteEndereco = await _clienteEnderecoRepository.ObterPorIdAsync(clienteEnderecoId)
            ?? throw new NotFoundException("Cliente > Endereço", clienteEnderecoId);

        clienteEndereco.StatusRegistro = StatusRegistro.Excluido;
        await _clienteEnderecoRepository.AtualizarAsync(clienteEndereco);
    }
}