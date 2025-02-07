using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
  private readonly AppDbContext _context;

  public UsuarioController(AppDbContext context)
  {
    _context = context;
  }

  // [Authorize]
  [HttpPost("cadastrar")]
  public async Task<IActionResult> Cadastrar([FromBody] Usuario usuario)
  {
    // Validação de campos obrigatórios
    if (string.IsNullOrEmpty(usuario.Nome) || string.IsNullOrEmpty(usuario.CPF))
    {
      return BadRequest("Nome e CPF são obrigatórios!");
    }


    _context.Usuarios.Add(usuario);
    await _context.SaveChangesAsync();

    string cpfPrefix = usuario.CPF.Substring(0, 4);

    // Retorna a mensagem com sucesso
    return Ok($"Pessoa cadastrada com sucesso, código {cpfPrefix}");
  }

  [HttpGet("listar")]
  public async Task<IActionResult> ListarUsuarios()
  {
    try
    {

      var usuarios = await _context.Usuarios.ToListAsync();

      if (usuarios == null || usuarios.Count == 0)
      {
        return NotFound(new { mensagem = "Nenhum usuário encontrado." });
      }

      return Ok(usuarios);
    }
    catch (Exception ex)
    {

      return StatusCode(500, new { mensagem = "Erro ao listar usuários", erro = ex.Message });
    }
  }
}
