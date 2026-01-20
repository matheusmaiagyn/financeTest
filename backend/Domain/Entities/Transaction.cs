using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
  public class Transaction
  {
    public Guid ID { get; private set; }
    public string Description { get; private set; }
    public decimal Amount { get; private set; }
    public TransactionType TransactionType { get; private set; }
    public Guid CategoryID { get; private set; }
    public Category Category { get; private set; } // Navigation property
    public Guid UserID { get; private set; }
    public User User { get; private set; } // Navigation property

    protected Transaction() { }

    private Transaction(string description, decimal amount, TransactionType transactionType, Guid categoryID, Guid userID)
    {
      SetDescription(description);
      SetAmount(amount);
      SetUserID(userID);
      SetTransactionType(transactionType);
      SetCategoryID(categoryID);
    }

    public static Transaction Create(string description, decimal amount, TransactionType transactionType, Guid categoryID, Guid userID)
    {
      return new Transaction(description, amount, transactionType, categoryID, userID);
    }

    private void SetDescription(string description)
    {
      if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Descrição não pode ser vazia.");
      Description = description;
    }

    private void SetAmount(decimal amount)
    {
      if (amount <= 0) throw new ArgumentException("Valor não pode ser negativo.");
      Amount = amount;
    }

    private void SetTransactionType(TransactionType transactionType)
    {
      if (Enum.IsDefined(typeof(TransactionType), transactionType) == false) throw new ArgumentException("Tipo de transação inválido.");
      TransactionType = transactionType;
    }

    private void SetCategoryID(Guid categoryID)
    {
      if (categoryID == Guid.Empty) throw new ArgumentException("CategoryID não pode ser vazio.");
      CategoryID = categoryID;
    }

    private void SetUserID(Guid userID)
    {
      if (userID == Guid.Empty) throw new ArgumentException("UserID não pode ser vazio.");
      UserID = userID;
    }
  }
}
