using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemaAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SistemaAPI.Services;

namespace SistemaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
  private readonly AppDbContext _context;
  private readonly IConfiguration _configuration;

  public AuthController(AppDbContext context, IConfiguration configuration)
  {
    _context = context;
    _configuration = configuration;
  }

  // Método para autenticar login
  [HttpPost]
  public async Task<IActionResult> Login([FromBody] Login login)
  {
    var user = await _context.Logins.FirstOrDefaultAsync(l => l.Usuario == login.Usuario);
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

  // Método para criar login
  [HttpPost("Criar")]
  public async Task<IActionResult> CriarLogin([FromBody] Login login)
  {
    var existingLogin = await _context.Logins.FirstOrDefaultAsync(l => l.Usuario == login.Usuario);

    if (existingLogin != null)
    {
      return BadRequest(new { mensagem = "Este login já existe." });
    }

    var passwordHasher = new PasswordHasher<Login>();
    login.Senha = passwordHasher.HashPassword(login, login.Senha); // Criptografa a senha

    await _context.Logins.AddAsync(login);
    await _context.SaveChangesAsync();

    return Ok(new { mensagem = "Login criado com sucesso." });
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


}
