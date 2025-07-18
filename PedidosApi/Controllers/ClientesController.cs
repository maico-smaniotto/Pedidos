using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PedidosAPI.DTOs;
using PedidosAPI.Services.Interfaces;

namespace PedidosAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(
        [FromQuery, Range(1, int.MaxValue)] int page = 1,
        [FromQuery, Range(1, 100)] int pageSize = 10,
        [FromQuery] char? tipoPessoa = null,
        [FromQuery] string? nome = null,
        [FromQuery] bool ativo = true)
    {
        // if (page <= 0 || pageSize <= 0)
        // {
        //     return BadRequest("Parâmetros de paginação inválidos.");
        // }
        var result = await _clienteService.ListarPaginadoAsync(page, pageSize, nome, tipoPessoa, ativo);

        return result.Content.Any()
            ? Ok(result)
            : NoContent();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromRoute] long id)
    {
        var result = await _clienteService.ObterPorIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(
        [FromBody] ClienteCreateRequest request)
    {
        var result = await _clienteService.CadastrarAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(
        [FromRoute] long id,
        [FromBody] ClienteUpdateRequest request)
    {
        var result = await _clienteService.AtualizarAsync(id, request);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromRoute] long id)
    {
        await _clienteService.RemoverAsync(id);
        return NoContent();
    }
}