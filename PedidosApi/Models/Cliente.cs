using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PedidosAPI.Enums;

namespace PedidosAPI.Models;

[Table("clientes")]
public class Cliente
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("tipo_pessoa")]
    public TipoPessoa TipoPessoa { get; set; }

    [MaxLength(18)]
    [Column("cpf_cnpj")]
    public required string CpfCnpj { get; set; }

    [MaxLength(120)]
    [Column("razao_social")]
    public required string RazaoSocial { get; set; }

    [MaxLength(60)]
    [Column("nome_fantasia")]
    public required string NomeFantasia { get; set; }

    [MaxLength(255)]
    [Column("email")]
    public string? Email { get; set; }

    [Column("status_registro")]
    public StatusRegistro StatusRegistro { get; set; }

    public ICollection<ClienteEndereco> Enderecos { get; set; } = new List<ClienteEndereco>();

}