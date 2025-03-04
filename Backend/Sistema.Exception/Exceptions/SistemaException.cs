using System.Net;

namespace Sistema.Exception;

public abstract class SistemaException : SystemException
{
    public abstract List<string> GetErrorsMessages();
    public abstract HttpStatusCode GetHttpStatusCode();
}