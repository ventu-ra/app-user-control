using System.Collections.Generic;

namespace SistemaAPI.Models;
public class Login
{
  public int Id { get; set; }
  public string Usuario { get; set; }
  public string Senha { get; set; }

  public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
