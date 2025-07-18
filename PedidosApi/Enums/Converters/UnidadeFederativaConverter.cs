namespace PedidosAPI.Enums.Converters;

public static class UnidadeFederativaConverter
{
    public static string GetValor(UnidadeFederativa uf)
    {
        var attr = GetAttribute(uf);
        return attr?.Valor ?? throw new InvalidOperationException($"UnidadeFederativa '{uf}' não possui atributo com valor definido.");
    }

    public static string GetDescricao(UnidadeFederativa uf)
    {
        var attr = GetAttribute(uf);
        return attr?.Descricao ?? uf.ToString();
    }

    public static UnidadeFederativa FromValor(string valor)
    {
        var uf = Enum.GetValues<UnidadeFederativa>()
            .Cast<UnidadeFederativa?>()
            .FirstOrDefault(u => u.HasValue && GetValor(u.Value) == valor);

        if (uf == null)
            throw new ArgumentOutOfRangeException(nameof(valor), valor, "Valor inválido para UnidadeFederativa.");

        return uf.Value;
    }

    public static UnidadeFederativa FromDescricao(string descricao)
    {
        var uf = Enum.GetValues<UnidadeFederativa>()
            .Cast<UnidadeFederativa?>()
            .FirstOrDefault(u => u.HasValue && GetDescricao(u.Value) == descricao);

        if (uf == null)
            throw new ArgumentOutOfRangeException(nameof(descricao), descricao, "Valor inválido para UnidadeFederativa.");

        return uf.Value;
    }

    private static EnumStringStringMetadataAttribute GetAttribute(UnidadeFederativa uf)
    {
        var field = uf.GetType().GetField(uf.ToString());

        var attribute = Attribute.GetCustomAttribute(field!, typeof(EnumStringStringMetadataAttribute))
                    as EnumStringStringMetadataAttribute;

        if (attribute == null)
            throw new InvalidOperationException($"O campo '{uf}' não possui o atributo EnumStringStringMetadataAttribute.");

        return attribute;
    }
}