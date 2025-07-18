namespace PedidosAPI.Enums;

public class EnumCharStringMetadataAttribute : Attribute
{
    public char Valor { get; }
    public string Descricao { get; }

    public EnumCharStringMetadataAttribute(char valor, string descricao)
    {
        Valor = valor;
        Descricao = descricao;
    }
}