using System.Runtime.InteropServices;
using Communication.Requests;
using Communication.Response;
using Microsoft.AspNetCore.Mvc;
using Sistema.API.UseCase.Users;

namespace Sistema.API.Controllers;

[Route("create/[controller]")]
[ApiController]
public class AuthsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredAuthJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromBody] RequestAuthJson request)
    {
        var useCase = new RegisterAuthUseCase();

        var response = useCase.Execute(request);

        return Created(string.Empty, response);
    }
}