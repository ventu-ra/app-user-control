using Communication.Requests;
using Communication.Response;
using Sistema.API.Infrastructure.Security.Tokens;
using Sistema.Exception;
using Sistema.API.Infrastructure.DataAccess;
using Sistema.API.Infrastructure.Security.CryptAlgorithm;

namespace Sistema.API.UseCase.Login;

public class LoginUseCase
{
    public ResponseRegisteredAuthJson Execute(RequestAuthJson request)
    {
        var dbContext = new AppDbContext();
        var entity = dbContext.Auths.FirstOrDefault(auth  => auth.Name.Equals(request.Name));

        if (entity == null)
            throw new InvalidLoginException();

        var cryptography = new BCryptAlgorithm();
        
        var passwordIsValid = cryptography.VerifyHashedPassword(request.Password, entity);
        if (passwordIsValid == false)
            throw new InvalidLoginException();
        
        var tokenGenerator = new JwtTokenGenerator();
        
        return new ResponseRegisteredAuthJson
        {
            Name = entity.Name,
            AccessToken = tokenGenerator.GenerateToken(entity),
        };
    }
}