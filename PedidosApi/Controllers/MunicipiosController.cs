using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PedidosAPI.Services.Interfaces;

namespace PedidosAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class MunicipiosController : ControllerBase
{
    private readonly IMunicipioService _municipioService;

    public MunicipiosController(IMunicipioService municipioService)
    {
        _municipioService = municipioService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(
        [FromQuery, Range(1, int.MaxValue)] int page = 1,
        [FromQuery, Range(1, 100)] int pageSize = 10,
        [FromQuery] string? uf = null,
        [FromQuery] string? nome = null)
    {
        var result = await _municipioService.ListarPaginadoAsync(page, pageSize, uf, nome);
        return result.Content.Any()
            ? Ok(result)
            : NoContent();
    }
}