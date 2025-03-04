
using System.ComponentModel.DataAnnotations;

namespace Sistema.API.Entity;

public class User : EntityBase
{
  [Required]
  public string Nome { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  [Required]
  public string CPF { get; set; } = string.Empty;
  public string Endereco { get; set; } = string.Empty;
  public string Telefone { get; set; } = string.Empty;

}
