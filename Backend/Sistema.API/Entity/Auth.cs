namespace Sistema.API.Entity;

public class Auth : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}