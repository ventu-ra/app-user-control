
using System.ComponentModel.DataAnnotations;

namespace Sistema.API.Entity;
public class EntityBase
{
  [Key]
  public Guid Id { get; set; } = Guid.NewGuid();

}
