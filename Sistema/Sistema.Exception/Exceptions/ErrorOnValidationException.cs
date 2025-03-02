using System.Net;

namespace Sistema.Exception;

public class ErrorOnValidationException : SistemaException
{
    private readonly List<string> _errors;

    public ErrorOnValidationException(List<string> errorsMessages)
    {
        _errors = errorsMessages;
    }

    public override List<string> GetErrorsMessages() => _errors;

    public override HttpStatusCode GetHttpStatusCode() => HttpStatusCode.BadRequest;
}