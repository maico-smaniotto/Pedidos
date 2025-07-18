using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PedidosAPI.Enums;
using PedidosAPI.Enums.Converters;
using PedidosAPI.Models;

namespace PedidosAPI.Repositories;

public class AppDbContext : DbContext
{
    public DbSet<Municipio> Municipios { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<ClienteEndereco> ClientesEnderecos { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var statusRegistroValueConverter = new ValueConverter<StatusRegistro, char>(
            v => StatusRegistroConverter.GetValor(v),
            v => StatusRegistroConverter.FromValor(v)
        );

        var unidadeFederativaValueConverter = new ValueConverter<UnidadeFederativa, string>(
            v => UnidadeFederativaConverter.GetValor(v),
            v => UnidadeFederativaConverter.FromValor(v)
        );

        var tipoPessoaValueConverter = new ValueConverter<TipoPessoa, char>(
            v => TipoPessoaConverter.GetValor(v),
            v => TipoPessoaConverter.FromValor(v)
        );

        modelBuilder.Entity<Municipio>()
            .Property(m => m.Uf)
            .HasConversion(unidadeFederativaValueConverter);

        modelBuilder.Entity<Cliente>()
            .Property(c => c.TipoPessoa)
            .HasConversion(tipoPessoaValueConverter);

        modelBuilder.Entity<Cliente>()
            .Property(c => c.StatusRegistro)
            .HasConversion(statusRegistroValueConverter);

        modelBuilder.Entity<ClienteEndereco>()
            .HasOne(ce => ce.Municipio)
            .WithMany()
            .HasForeignKey(ce => ce.MunicipioCodigoIbge)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ClienteEndereco>()
            .HasOne(ce => ce.Cliente)
            .WithMany(c => c.Enderecos) // Para navegação reversa (Cliente com lista de endereços)
            .HasForeignKey(ce => ce.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}