using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaAPI.Services
{
  public class JwtTokenService
  {
    private readonly string _secretKey = "minhasecretnotavisivelparatodos1234567890";
    private readonly string _issuer = "sistema.com";
    private readonly string _audience = "sistema.com";

    public string GenerateJwtToken(string username)
    {
      // Definir as Claims do token
      var claims = new[]
      {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

      // Criar a chave de segurança com o segredo
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      // Gerar o token
      var token = new JwtSecurityToken(
          issuer: _issuer,
          audience: _audience,
          claims: claims,
          expires: DateTime.Now.AddMinutes(30),
          signingCredentials: creds);

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}
