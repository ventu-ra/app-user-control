using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CadastroController : ControllerBase
{
  private readonly AppDbContext _context;

  public CadastroController(AppDbContext context)
  {
    _context = context;
  }

  [Authorize]
  [HttpPost("Cadastrar")]
  public IActionResult CadastrarUsuario([FromBody] Usuario usuario)
  {
    if (usuario == null)
    {
      return BadRequest("Usuário não enviado.");
    }

    // Verificar se os campos necessários estão presentes
    if (string.IsNullOrEmpty(usuario.Nome) || string.IsNullOrEmpty(usuario.CPF))
    {
      return BadRequest("Nome e CPF são obrigatórios.");
    }

    _context.Usuarios.Add(usuario);
    _context.SaveChanges();

    return Ok(new { mensagem = "Usuário cadastrado com sucesso!" });
  }
  
  [Authorize]
  [HttpGet("Listar")]
  public IActionResult ListarUsuarios()
  {
    var usuarios = _context.Usuarios.ToList(); // Obtém todos os usuários
    return Ok(usuarios); // Retorna os usuários em formato JSON
  }

}
