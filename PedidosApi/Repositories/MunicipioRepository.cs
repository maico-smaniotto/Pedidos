using Microsoft.EntityFrameworkCore;
using PedidosAPI.Enums;
using PedidosAPI.Models;
using PedidosAPI.Repositories.Interfaces;

namespace PedidosAPI.Repositories;

public class MunicipioRepository : IMunicipioRepository
{
    private readonly AppDbContext _context;

    public MunicipioRepository(AppDbContext context)
    {
        _context = context;
    }

    private IQueryable<Municipio> ObterQuery(UnidadeFederativa? uf, string? nome)
    {
        var query = _context.Municipios.AsQueryable();

        if (uf != null)
        {
            query = query
                .Where(m => m.Uf == uf);
        }

        if (!string.IsNullOrWhiteSpace(nome))
        {
            query = query
                .Where(m => m.Nome.ToLower().Contains(nome.ToLower()));
        }

        return query;
    }

    public async Task<IEnumerable<Municipio>> ObterTodosAsync(UnidadeFederativa? uf, string? nome)
    {
        var query = ObterQuery(uf, nome);

        return await query
            .OrderBy(m => m.Uf)
            .ThenBy(m => m.Nome)
            .ToListAsync();
    }

    public async Task<IEnumerable<Municipio>> ObterPaginadoAsync(int page, int pageSize, UnidadeFederativa? uf, string? nome)
    {
        var query = ObterQuery(uf, nome);

        return await query
            .OrderBy(m => m.Uf)
            .ThenBy(m => m.Nome)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Municipio?> ObterPorIdAsync(string id)
    {
        return await _context.Municipios
            .FirstOrDefaultAsync(m => m.CodigoIbge == id);
    }

    public async Task<Municipio> AdicionarAsync(Municipio municipio)
    {
        await _context.Municipios.AddAsync(municipio);
        await _context.SaveChangesAsync();
        return municipio;
    }

    public async Task<Municipio> AtualizarAsync(Municipio municipio)
    {
        _context.Municipios.Update(municipio);
        await _context.SaveChangesAsync();
        return municipio;
    }

    public async Task RemoverAsync(string id)
    {
        var municipio = await ObterPorIdAsync(id);

        if (municipio != null)
        {
            _context.Municipios.Remove(municipio);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExisteAsync(string id)
    {
        return await _context.Municipios.AnyAsync(m => m.CodigoIbge == id);
    }

    public async Task<int> ContarTodosAsync(UnidadeFederativa? uf, string? nome)
    {
        var query = ObterQuery(uf, nome);

        return await query.CountAsync();
    }
}