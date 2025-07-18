namespace PedidosAPI.Enums;

public class EnumStringStringMetadataAttribute : Attribute
{
    public string Valor { get; }
    public string Descricao { get; }

    public EnumStringStringMetadataAttribute(string valor, string descricao)
    {
        Valor = valor;
        Descricao = descricao;
    }
}