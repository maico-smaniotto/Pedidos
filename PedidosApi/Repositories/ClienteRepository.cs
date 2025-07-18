using Microsoft.EntityFrameworkCore;
using PedidosAPI.Enums;
using PedidosAPI.Models;
using PedidosAPI.Repositories.Interfaces;

namespace PedidosAPI.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }

    private IQueryable<Cliente> ObterQuery(string? nome, TipoPessoa? tipoPessoa, bool ativo = true)
    {
        var query = _context.Clientes.AsQueryable();

        if (tipoPessoa != null)
        {
            query = query
                .Where(c => c.TipoPessoa == tipoPessoa);
        }

        if (!string.IsNullOrWhiteSpace(nome))
        {
            query = query
                .Where(c => c.NomeFantasia.ToLower().Contains(nome.ToLower()) ||
                            c.RazaoSocial.ToLower().Contains(nome.ToLower()));
        }

        StatusRegistro status = ativo ? StatusRegistro.Ativo : StatusRegistro.Inativo;
        query = query
            .Where(c => c.StatusRegistro == status);

        return query;
    }

    public async Task<IEnumerable<Cliente>> ObterPaginadoAsync(int page, int pageSize, string? nome, TipoPessoa? tipoPessoa, bool ativo = true)
    {
        var query = ObterQuery(nome, tipoPessoa, ativo);

        return await query
            .OrderBy(c => c.NomeFantasia)
            .ThenBy(c => c.RazaoSocial)
            .ToListAsync();
    }

    public async Task<Cliente?> ObterPorIdAsync(long id)
    {
        return await _context.Clientes
            .Include(c => c.Enderecos)
            .FirstOrDefaultAsync(c => c.Id == id && c.StatusRegistro != StatusRegistro.Excluido);
    }

    public async Task<Cliente> AdicionarAsync(Cliente cliente)
    {
        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task<Cliente> AtualizarAsync(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task RemoverAsync(Cliente cliente)
    {
        cliente.StatusRegistro = StatusRegistro.Excluido;
        _context.Update(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExisteAsync(long id)
    {
        return await _context.Clientes.AnyAsync(c => c.Id == id && c.StatusRegistro != StatusRegistro.Excluido);
    }

    public async Task<int> ContarTodosAsync(string? nome, TipoPessoa? tipoPessoa, bool ativo = true)
    {
        var query = ObterQuery(nome, tipoPessoa, ativo);

        return await query.CountAsync();
    }
}