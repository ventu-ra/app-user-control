using Communication.Requests;
using FluentValidation;

namespace Sistema.API.UseCase.Users;

public class RegisterUserValidator : AbstractValidator<RequestUserJson>
{
  public RegisterUserValidator()
  {
    RuleFor(request => request.Nome).NotEmpty().WithMessage("Name is required");
    RuleFor(request => request.Email).EmailAddress().WithMessage("Email is not valid");
    RuleFor(request => request.CPF).NotEmpty().WithMessage("CPF is required");
  }

}
