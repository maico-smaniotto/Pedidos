namespace PedidosAPI.Enums;

public enum StatusRegistro
{
    [EnumCharStringMetadata('A', "Ativo")]
    Ativo,

    [EnumCharStringMetadata('I', "Inativo")]
    Inativo,

    [EnumCharStringMetadata('X', "Excluído")]
    Excluido
}