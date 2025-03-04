using System.Net;

namespace Sistema.Exception;

public class InvalidLoginException : SistemaException
{
    public override List<string> GetErrorsMessages() => ["Nome e/ou senha invalido."];

    public override HttpStatusCode GetHttpStatusCode() => HttpStatusCode.Unauthorized;
}