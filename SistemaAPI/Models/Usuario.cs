namespace SistemaAPI.Models;
public class Usuario
{
  public int Id { get; set; } // Código
  public string Nome { get; set; }
  public string CPF { get; set; }
  public string Endereco { get; set; }
  public string Telefone { get; set; }

  // Relacionamento com a tabela Login
  public int LoginId { get; set; }
  public Login Login { get; set; }

}
