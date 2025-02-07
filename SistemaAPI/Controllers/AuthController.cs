using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SistemaAPI.Models;

namespace SistemaAPI.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private const string USERNAME = "SISTEMA";
    private const string PASSWORD = "canditado123";
    private const string SECRET_KEY = "minhasecretnotavisivelparatodos1234567890";

    [HttpPost("login")]
    public IActionResult Login([FromBody] Login request)
    {
        if (request.Username == USERNAME && request.Password == PASSWORD)
        {
            var token = GenerateJwtToken(request.Username);
            return Ok(new { Token = token });
        }
        return Unauthorized(new { Message = "Usuário ou senha inválidos" });
    }

    private string GenerateJwtToken(string username)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: "meusistema.com",
            audience: "meusistema.com",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok(new { message = "API funcionando!" });
    }
}

