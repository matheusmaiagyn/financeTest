using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Factories
{
  public sealed class TransactionFactory : ITransactionFactory
  {
    public TransactionFactory() { }

    public Transaction Create(User user, Category category, string description, decimal amount, TransactionType transactionType)
    {
      if (user.Age < 18 && transactionType == TransactionType.Income)
        throw new BusinessRuleException("Usuários menores de 18 não podem ter receitas.");

      if (transactionType == TransactionType.Expense && category.CategoryType == CategoryType.Income)
        throw new BusinessRuleException("Categoria não permite despesa.");

      if (transactionType == TransactionType.Income && category.CategoryType == CategoryType.Expense)
        throw new BusinessRuleException("Categoria não permite receita.");

      return Transaction.Create(description, amount, transactionType, category.ID, user.ID);
    }
  }
}
