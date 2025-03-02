using Communication.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sistema.Exception;

namespace Sistema.API.Filters;

public class ExceptionsFilter : IExceptionFilter
{
  public void OnException(ExceptionContext context)
  {
    if (context.Exception is SistemaException sistemaAPIException)
    {
      context.HttpContext.Response.StatusCode = (int)sistemaAPIException.GetHttpStatusCode();
      context.Result = new ObjectResult(new ResponseErrorMessagesJson(sistemaAPIException.GetErrorsMessages()));
    }
    else
    {
      ThrowUnKownError(context);
    }
  }

  private void ThrowUnKownError(ExceptionContext context)
  {
    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
    context.Result = new ObjectResult(new ResponseErrorMessagesJson("Error desconhecido."));
  }
}
