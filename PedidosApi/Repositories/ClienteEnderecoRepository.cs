using Microsoft.EntityFrameworkCore;
using PedidosAPI.Enums;
using PedidosAPI.Models;
using PedidosAPI.Repositories.Interfaces;

namespace PedidosAPI.Repositories;

public class ClienteEnderecoRepository : IClienteEnderecoRepository
{
    private readonly AppDbContext _context;

    public ClienteEnderecoRepository(AppDbContext context)
    {
        _context = context;
    }

    private IQueryable<ClienteEndereco> ObterQuery(long clienteId, bool ativo = true)
    {
        var query = _context.ClientesEnderecos.AsQueryable();

        StatusRegistro status = ativo ? StatusRegistro.Ativo : StatusRegistro.Inativo;
        query = query
            .Where(ce => ce.ClienteId == clienteId)
            .Where(ce => ce.StatusRegistro == status);

        return query;
    }

    public async Task<IEnumerable<ClienteEndereco>> ObterPaginadoAsync(int page, int pageSize, long clienteId, bool ativo = true)
    {
        var query = ObterQuery(clienteId, ativo);

        return await query
            .OrderBy(ce => ce.PadraoFaturamento)
            .ThenBy(ce => ce.PadraoEntrega)
            .ToListAsync();
    }

    public async Task<ClienteEndereco?> ObterPorIdAsync(long id)
    {
        return await _context.ClientesEnderecos
            .FirstOrDefaultAsync(ce => ce.Id == id);
    }

    public async Task<ClienteEndereco> AdicionarAsync(ClienteEndereco clienteEndereco)
    {
        await _context.ClientesEnderecos.AddAsync(clienteEndereco);
        await _context.SaveChangesAsync();
        return clienteEndereco;
    }

    public async Task<ClienteEndereco> AtualizarAsync(ClienteEndereco clienteEndereco)
    {
        _context.ClientesEnderecos.Update(clienteEndereco);
        await _context.SaveChangesAsync();
        return clienteEndereco;
    }

    public async Task RemoverAsync(long id)
    {
        var clienteEndereco = await ObterPorIdAsync(id);
        if (clienteEndereco != null)
        {
            clienteEndereco.StatusRegistro = StatusRegistro.Excluido;
            _context.ClientesEnderecos.Update(clienteEndereco);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExisteAsync(long id)
    {
        return await _context.ClientesEnderecos.AnyAsync(ce => ce.Id == id);
    }

    public async Task<int> ContarTodosAsync(long clienteId, bool ativo = true)
    {
        var query = ObterQuery(clienteId, ativo);

        return await query.CountAsync();
    }
}