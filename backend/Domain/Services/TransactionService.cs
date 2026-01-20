using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
  public class TransactionService : BaseService<Transaction>, ITransactionService
  {
    private ITransactionRepository transactionRepository => (ITransactionRepository)BaseRepository;

    public TransactionService(ITransactionRepository transactionRepository)
      : base(transactionRepository)
    {
    }
  }
}
