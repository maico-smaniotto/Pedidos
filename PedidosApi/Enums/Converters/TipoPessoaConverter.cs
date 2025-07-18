namespace PedidosAPI.Enums.Converters;

public static class TipoPessoaConverter
{
    public static char GetValor(TipoPessoa tipoPessoa)
    {
        var attr = GetAttribute(tipoPessoa);
        return attr?.Valor ?? throw new InvalidOperationException($"TipoPessoa '{tipoPessoa}' não possui atributo com valor definido.");
    }

    public static string GetDescricao(TipoPessoa tipoPessoa)
    {
        var attr = GetAttribute(tipoPessoa);
        return attr?.Descricao ?? tipoPessoa.ToString();
    }

    public static TipoPessoa FromValor(char valor)
    {
        var tipoPessoa = Enum.GetValues<TipoPessoa>()
            .Cast<TipoPessoa?>()
            .FirstOrDefault(t => t.HasValue && GetValor(t.Value) == valor);

        if (tipoPessoa == null)
            throw new ArgumentOutOfRangeException(nameof(valor), valor, "Valor inválido para TipoPessoa.");

        return tipoPessoa.Value;
    }

    public static TipoPessoa FromDescricao(string descricao)
    {
        var tipoPessoa = Enum.GetValues<TipoPessoa>()
            .Cast<TipoPessoa?>()
            .FirstOrDefault(t => t.HasValue && GetDescricao(t.Value) == descricao);

        if (tipoPessoa == null)
            throw new ArgumentOutOfRangeException(nameof(descricao), descricao, "Valor inválido para TipoPessoa.");

        return tipoPessoa.Value;
    }

    private static EnumCharStringMetadataAttribute GetAttribute(TipoPessoa tipoPessoa)
    {
        var field = tipoPessoa.GetType().GetField(tipoPessoa.ToString());

        var attribute = Attribute.GetCustomAttribute(field!, typeof(EnumCharStringMetadataAttribute))
                    as EnumCharStringMetadataAttribute;

        if (attribute == null)
            throw new InvalidOperationException($"O campo '{tipoPessoa}' não possui o atributo EnumCharStringMetadataAttribute.");

        return attribute;        
    }
    
}