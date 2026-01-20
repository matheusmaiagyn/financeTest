using Application.DTOs.Request.Models;
using FluentValidation;

namespace Application.DTOs.Request.Validators
{
  public class AddUserRequestModelValidator : AbstractValidator<AddUserRequestModel>
  {
    public AddUserRequestModelValidator()
    {
      RuleFor(x => x.Name)
        .NotEmpty().WithMessage("É necessário informar um nome.")
        .MaximumLength(100).WithMessage("O máximo são 100 caracteres para o nome.");
      RuleFor(x => x.Age)
        .InclusiveBetween(0, 150).WithMessage("Idade precisa ser entre 0 e 100");
    }
  }
}
