
using System.Runtime.InteropServices;
using Communication.Requests;
using Communication.Response;
using FluentValidation.Results;
using Sistema.API.Entity;
using Sistema.API.Infrastructure.Security.Tokens;
using Sistema.Exception;
using Sistema.API.Infrastructure.DataAccess;
using Sistema.API.Infrastructure.Security.CryptAlgorithm;

namespace Sistema.API.UseCase.Users;

public class RegisterAuthUseCase
{
  public ResponseRegisteredAuthJson Execute(RequestAuthJson request)
  {


    var dbContext = new AppDbContext();
    Validate(request, dbContext);

    var cryptography = new BCryptAlgorithm();

    var entity = new Auth()
    {
      Name = request.Name,
      // Password = request.Password,
      Password = cryptography.HashPassword(request.Password),
    };

    dbContext.Auths.AddAsync(entity);
    dbContext.SaveChangesAsync();

    var tokenGenerator = new JwtTokenGenerator();

    return new ResponseRegisteredAuthJson
    {
      Name = entity.Name,
      AccessToken = tokenGenerator.GenerateToken(entity)
    };
  }

  private void Validate(RequestAuthJson request, AppDbContext dbContext)
  {


    var validator = new RegisterAuthValidator();

    var result = validator.Validate(request);

    var existAuthWithName = dbContext.Auths.Any(auth => auth.Name.Equals(request.Name));

    if (existAuthWithName)
      result.Errors.Add(new ValidationFailure("Name", "Name is already in use."));

    if (result.IsValid is false)
    {
      var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errorMessages);
    }
  }
}
