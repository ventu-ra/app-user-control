using Communication.Requests;
using FluentValidation;

namespace Sistema.API.UseCase.Users;

public class RegisterAuthValidator : AbstractValidator<RequestAuthJson>
{
  public RegisterAuthValidator()
  {
    RuleFor(request => request.Name).NotEmpty().WithMessage("Name is required");
    RuleFor(request => request.Password).NotEmpty().WithMessage("Password is required");
    When(request => string.IsNullOrEmpty(request.Password), () =>
    {
      RuleFor(request => request.Password.Length).GreaterThanOrEqualTo(6)
              .WithMessage("Senha deve ter mais de 6 caracteres.");
    });
  }

}
