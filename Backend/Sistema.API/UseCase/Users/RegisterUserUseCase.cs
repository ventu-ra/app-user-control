
using Communication.Requests;
using Communication.Response;
using FluentValidation.Results;
using Sistema.API.Entity;
using Sistema.Exception;
using Sistema.API.Infrastructure.DataAccess;

namespace Sistema.API.UseCase.Users;


public class RegisterUserUseCase
{
  public ResponseUserJson Execute(RequestUserJson request)
  {
    var dbContext = new AppDbContext();
    
    Validate(request, dbContext);


    var entity = new User()
    {
      Nome = request.Nome,
      Email = request.Email,
      CPF =  request.CPF,
      Endereco = request.Endereco,
      Telefone = request.Telefone,
    };

    dbContext.Users.Add(entity);
    dbContext.SaveChanges();

    return new ResponseUserJson
    {
      Nome = entity.Nome,
      Email = entity.Email,
    };
  }

  private void Validate(RequestUserJson request, AppDbContext dbContext)
  {
    var validator = new RegisterUserValidator();
        
    var result = validator.Validate(request);
        
    var existUserWithEmail = dbContext.Users.Any(user => user.Email.Equals(request.Email));

    if (existUserWithEmail) 
      result.Errors.Add(new ValidationFailure("Email", "Email is already in use."));
        
    if (result.IsValid is false)
    {
      var errorMessages = result.Errors.Select(error  => error.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errorMessages);
    }

  }
}