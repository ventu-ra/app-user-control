using System.Runtime.InteropServices;
using Communication.Requests;
using Communication.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sistema.API.UseCase.Users;

namespace Sistema.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status400BadRequest)]
    [Route("create")]
    
    public IActionResult Register([FromBody] RequestUserJson request)
    {
        var useCase = new RegisterUserUseCase();

        var response = useCase.Execute(request);

        return Created(string.Empty, response);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult GetAll()
    {
        var useCase = new GetAllUsersUseCase();
        var response = useCase.Execute();

        if (response.Users.Count == 0) return NoContent();

        return Ok(response);
    }
}