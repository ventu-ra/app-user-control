using Communication.Requests;
using Microsoft.AspNetCore.Mvc;
using Sistema.API.UseCase.Login;

namespace Sistema.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    [HttpPost]
    public IActionResult Login(RequestAuthJson request)
    {
        var useCase = new LoginUseCase();
        
        var response = useCase.Execute(request);
        return Ok(response);
    }
}