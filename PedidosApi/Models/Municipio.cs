using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PedidosAPI.Enums;

namespace PedidosAPI.Models;

[Table("municipios")]
public class Municipio
{
    [Key]
    [MaxLength(7)]
    [Column("codigo_ibge")]
    public required string CodigoIbge { get; set; }

    [MaxLength(120)]
    [Column("nome")]
    public required string Nome { get; set; }

    [MaxLength(2)]
    [Column("uf")]
    public UnidadeFederativa Uf { get; set; }

}
