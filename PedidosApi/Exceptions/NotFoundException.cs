namespace PedidosAPI.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string entidade, object chave)
        : base($"{entidade} com identificador '{chave}' não foi encontrado.") { }
}