using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using PedidosAPI.Enums;

namespace PedidosAPI.Models;

[Table("clientes_enderecos")]
public class ClienteEndereco
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [MaxLength(100)]
    [Column("logradouro")]
    public required string Logradouro { get; set; }

    [MaxLength(20)]
    [Column("numero")]
    public required string Numero { get; set; }

    [MaxLength(120)]
    [Column("complemento")]
    public string? Complemento { get; set; }

    [MaxLength(60)]
    [Column("bairro")]
    public required string Bairro { get; set; }

    [Column("municipio_codigo_ibge")]
    public required string MunicipioCodigoIbge { get; set; }
    public required Municipio Municipio { get; set; }

    [MaxLength(9)]
    [Column("codigo_postal")]
    public required string CodigoPostal { get; set; }

    [Column("padrao_faturamento")]
    public bool PadraoFaturamento { get; set; }

    [Column("padrao_entrega")]
    public bool PadraoEntrega { get; set; }

    [Column("status_registro")]
    public StatusRegistro StatusRegistro { get; set; }

    [Column("cliente_id")]
    public long ClienteId { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(ClienteId))]
    public required Cliente Cliente { get; set; }

}