
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Sistema.API.Entity;

namespace Sistema.API.Infrastructure.Security.Tokens;
public class JwtTokenGenerator
{
  public string GenerateToken(Auth user)
  {
    var claims = new List<Claim>()
    {
      new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
    };

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Expires = DateTime.UtcNow.AddMinutes(60),
      Subject = new ClaimsIdentity(claims),
      SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature)
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    var securityToken = tokenHandler.CreateToken(tokenDescriptor);

    return tokenHandler.WriteToken(securityToken);
  }

  private static SymmetricSecurityKey SecurityKey()
  {
    const string signingKey = "gLTNsqGpoLEVzTnKkBHNTYbnOxjPJHak";
    var symmetricKey = Encoding.UTF8.GetBytes(signingKey);

    return new SymmetricSecurityKey(symmetricKey);
  }

}
