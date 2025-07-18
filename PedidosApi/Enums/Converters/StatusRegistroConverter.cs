namespace PedidosAPI.Enums.Converters;

public static class StatusRegistroConverter
{
    public static char GetValor(StatusRegistro statusRegistro)
    {
        var attr = GetAttribute(statusRegistro);
        return attr?.Valor ?? throw new InvalidOperationException($"StatusRegistro '{statusRegistro}' não possui atributo com valor definido.");
    }

    public static string GetDescricao(StatusRegistro statusRegistro)
    {
        var attr = GetAttribute(statusRegistro);
        return attr?.Descricao ?? statusRegistro.ToString();
    }

    public static StatusRegistro FromValor(char valor)
    {
         var statusRegistro = Enum.GetValues(typeof(StatusRegistro))
            .Cast<StatusRegistro>()
            .FirstOrDefault(s => GetValor(s) == valor);

        if (statusRegistro.Equals(default(StatusRegistro)) && !Enum.IsDefined(typeof(StatusRegistro), statusRegistro))
            throw new ArgumentOutOfRangeException(nameof(valor), valor, "Valor inválido para StatusRegistro.");

        return statusRegistro;
    }

    public static StatusRegistro FromDescricao(string descricao)
    {
        var statusRegistro = Enum.GetValues(typeof(StatusRegistro))
            .Cast<StatusRegistro>()
            .FirstOrDefault(t => GetDescricao(t) == descricao);

        if (statusRegistro.Equals(default(StatusRegistro)) && !Enum.IsDefined(typeof(StatusRegistro), statusRegistro))
            throw new ArgumentOutOfRangeException(nameof(descricao), descricao, "Valor inválido para StatusRegistro.");

        return statusRegistro;
    }

    private static EnumCharStringMetadataAttribute GetAttribute(StatusRegistro statusRegistro)
    {
        var field = statusRegistro.GetType().GetField(statusRegistro.ToString());

        var attribute = Attribute.GetCustomAttribute(field!, typeof(EnumCharStringMetadataAttribute))
                    as EnumCharStringMetadataAttribute;

        if (attribute == null)
            throw new InvalidOperationException($"O campo '{statusRegistro}' não possui o atributo EnumCharStringMetadataAttribute.");

        return attribute;
    }
}