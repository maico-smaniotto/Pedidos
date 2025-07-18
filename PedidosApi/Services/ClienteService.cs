using System.ComponentModel.DataAnnotations;
using PedidosAPI.DTOs;
using PedidosAPI.Enums;
using PedidosAPI.Enums.Converters;
using PedidosAPI.Exceptions;
using PedidosAPI.Models;
using PedidosAPI.Repositories.Interfaces;
using PedidosAPI.Services.Interfaces;

namespace PedidosAPI.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMunicipioRepository _municipioRepository;

    public ClienteService(IClienteRepository clienteRepository, IMunicipioRepository municipioRepository)
    {
        _clienteRepository = clienteRepository;
        _municipioRepository = municipioRepository;
    }

    public async Task<PageResponse<ClienteResponse>> ListarPaginadoAsync(int page, int pageSize, string? nome, char? tipoPessoa, bool ativo = true)
    {
        TipoPessoa? tipo = tipoPessoa.HasValue
            ? TipoPessoaConverter.FromValor(tipoPessoa.Value)
            : null;

        var content = await _clienteRepository.ObterPaginadoAsync(page, pageSize, nome, tipo, ativo);
        var totalElements = await _clienteRepository.ContarTodosAsync(nome, tipo, ativo);

        return PageResponse<ClienteResponse>.Create(
            ClienteResponse.ListFromEntityList(content),
            page,
            pageSize,
            totalElements
        );
    }

    public async Task<ClienteResponse> ObterPorIdAsync(long clienteId)
    {
        var cliente = await _clienteRepository.ObterPorIdAsync(clienteId) ?? throw new NotFoundException("Cliente", clienteId);
        return ClienteResponse.FromEntity(cliente);
    }

    public async Task<ClienteResponse> CadastrarAsync(ClienteCreateRequest clienteCreateRequest)
    {
        if (!clienteCreateRequest.Enderecos.Any())
            throw new BusinessException("O cliente deve possuir pelo menos um endereço.");

        var cliente = new Cliente
        {
            TipoPessoa = TipoPessoaConverter.FromValor(clienteCreateRequest.TipoPessoa),
            CpfCnpj = clienteCreateRequest.DocPessoa,
            RazaoSocial = clienteCreateRequest.RazaoSocial,
            NomeFantasia = !string.IsNullOrWhiteSpace(clienteCreateRequest.NomeFantasia) ? clienteCreateRequest.NomeFantasia : clienteCreateRequest.RazaoSocial,
            Email = clienteCreateRequest.Email,
            StatusRegistro = clienteCreateRequest.Ativo ? StatusRegistro.Ativo : StatusRegistro.Inativo
        };

        foreach (var endereco in clienteCreateRequest.Enderecos)
        {
            var municipio = await _municipioRepository.ObterPorIdAsync(endereco.MunicipioCodigoIbge) ?? throw new NotFoundException("Município", endereco.MunicipioCodigoIbge);

            cliente.Enderecos.Add(
                new ClienteEndereco
                {
                    Logradouro = endereco.Logradouro,
                    Numero = endereco.Numero,
                    Complemento = endereco.Complemento,
                    Bairro = endereco.Bairro,
                    CodigoPostal = endereco.Cep,
                    PadraoFaturamento = endereco.PadraoFaturamento,
                    PadraoEntrega = endereco.PadraoEntrega,
                    StatusRegistro = StatusRegistro.Ativo,
                    Cliente = cliente,
                    Municipio = municipio,
                    MunicipioCodigoIbge = endereco.MunicipioCodigoIbge
                }
            );
        }

        await _clienteRepository.AdicionarAsync(cliente);
        return ClienteResponse.FromEntity(cliente);
    }

    public async Task<ClienteResponse> AtualizarAsync(long clienteId, ClienteUpdateRequest clienteUpdateRequest)
    {
        var cliente = await _clienteRepository.ObterPorIdAsync(clienteId) ?? throw new NotFoundException("Cliente", clienteId);

        cliente.TipoPessoa = TipoPessoaConverter.FromValor(clienteUpdateRequest.TipoPessoa);
        cliente.CpfCnpj = clienteUpdateRequest.DocPessoa;
        cliente.RazaoSocial = clienteUpdateRequest.RazaoSocial;
        cliente.NomeFantasia = clienteUpdateRequest.NomeFantasia ?? clienteUpdateRequest.RazaoSocial;
        cliente.Email = clienteUpdateRequest.Email;
        cliente.StatusRegistro = clienteUpdateRequest.Ativo ? StatusRegistro.Ativo : StatusRegistro.Inativo;

        await _clienteRepository.AtualizarAsync(cliente);
        return ClienteResponse.FromEntity(cliente);
    }

    public async Task RemoverAsync(long clienteId)
    {
        var cliente = await _clienteRepository.ObterPorIdAsync(clienteId) ?? throw new NotFoundException("Cliente", clienteId);
        await _clienteRepository.RemoverAsync(cliente);
    }
}