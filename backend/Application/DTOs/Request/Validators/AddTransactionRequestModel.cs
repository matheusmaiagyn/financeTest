using Application.DTOs.Request.Models;
using FluentValidation;

namespace Application.DTOs.Request.Validators
{
  public class AddTransactionRequestModelValidator : AbstractValidator<AddTransactionRequestModel>
  {
    public AddTransactionRequestModelValidator()
    {
      RuleFor(x => x.Description)
        .NotEmpty().WithMessage("É necessário informar uma descrição")
        .MaximumLength(300).WithMessage("Descrição não pode exceder 300 caraqcteres");
      RuleFor(x => x.Amount)
        .GreaterThan(0).WithMessage("O valor da transação deve ser maior que zero");
      RuleFor(x => x.TransactionType)
        .IsInEnum().WithMessage("Tipo de transação inválido");
      RuleFor(x => x.CategoryID)
        .NotEmpty().WithMessage("É necessário informar a categoria da transação");
      RuleFor(x => x.UserID)
        .NotEmpty().WithMessage("É necessário informar o usuário da transação");
    }
  }
}
