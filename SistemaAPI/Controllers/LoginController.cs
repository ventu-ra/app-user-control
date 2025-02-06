using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaAPI.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
  private readonly AppDbContext _context;

  public LoginController(AppDbContext context)
  {
    _context = context;
  }

  // Método para autenticar login
  [HttpPost]
  public IActionResult Login([FromBody] Login login)
  {
    var user = _context.Logins.FirstOrDefault(l => l.Usuario == login.Usuario);
    if (user == null)
    {
      return Unauthorized(new { mensagem = "Usuário não encontrado." });
    }

    var passwordHasher = new PasswordHasher<Login>();
    var result = passwordHasher.VerifyHashedPassword(user, user.Senha, login.Senha);

    if (result == PasswordVerificationResult.Failed)
    {
      return Unauthorized(new { mensagem = "Senha incorreta." });
    }

    // Geração do Token
    var token = GenerateJwtToken(user.Usuario);
    return Ok(new { token });
  }

  private string GenerateJwtToken(string username)
  {
    var claims = new[]
    {
      new Claim(JwtRegisteredClaimNames.Sub, username),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("minhasecretnotavisivelparatodos1234567890"));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
      issuer: "sistema.com",
      audience: "sistema.com",
      claims: claims,
      expires: DateTime.Now.AddMinutes(30),
      signingCredentials: creds);

    return new JwtSecurityTokenHandler().WriteToken(token);
  }

  [HttpPost("CriarLogin")]
  public IActionResult CriarLogin([FromBody] Login login)
  {
    var existingLogin = _context.Logins.FirstOrDefault(l => l.Usuario == login.Usuario);

    if (existingLogin != null)
    {
      return BadRequest(new { mensagem = "Este login já existe." });
    }

    var passwordHasher = new PasswordHasher<Login>();
    login.Senha = passwordHasher.HashPassword(login, login.Senha); // Criptografa a senha

    _context.Logins.Add(login);
    _context.SaveChanges();

    return Ok(new { mensagem = "Login criado com sucesso." });
  }
}
