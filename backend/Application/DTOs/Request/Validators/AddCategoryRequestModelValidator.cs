using Application.DTOs.Request.Models;
using FluentValidation;

namespace Application.DTOs.Request.Validators
{
  public class AddCategoryRequestModelValidator : AbstractValidator<AddCategoryRequestModel>
  {
    public AddCategoryRequestModelValidator()
    {
      RuleFor(x => x.Description)
        .NotEmpty().WithMessage("É necessário informar uma descrição.")
        .MaximumLength(100).WithMessage("Descrição não pode exceder 100 caracteres");

      RuleFor(x => x.CategoryType)
        .NotEmpty().WithMessage("O tipo da categoria precisa ser informado");
    }
  }
}
