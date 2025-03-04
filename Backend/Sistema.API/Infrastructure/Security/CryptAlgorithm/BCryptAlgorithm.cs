
using Sistema.API.Entity;

namespace Sistema.API.Infrastructure.Security.CryptAlgorithm;
public class BCryptAlgorithm
{
  public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

  public bool VerifyHashedPassword(string hashPassword, Auth auth)
    => BCrypt.Net.BCrypt.Verify(hashPassword, auth.Password);
}
