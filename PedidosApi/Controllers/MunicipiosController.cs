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
    public async Task<IActionResult> Listar(
        [FromQuery, Range(1, int.MaxValue)] int page = 1,
        [FromQuery, Range(1, 100)] int pageSize = 10,
        [FromQuery] string? uf = null,
        [FromQuery] string? nome = null
    )
    {
        if (page <= 0 || pageSize <= 0)
        {
            return BadRequest("Parâmetros de paginação inválidos.");
        }
        var resultado = await _municipioService.ListarPaginadoAsync(page, pageSize, uf, nome);
        return Ok(resultado);
    }
}