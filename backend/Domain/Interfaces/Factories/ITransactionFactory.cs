using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces.Factories
{
  public interface ITransactionFactory
  {
    Transaction Create(User user, Category category, string description, decimal amount, TransactionType transactionType);
  }
}
