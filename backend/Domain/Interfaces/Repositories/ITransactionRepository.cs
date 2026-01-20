using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces.Repositories
{
  public interface ITransactionRepository : IBaseRepository<Transaction>
  {
    Task<IEnumerable<Transaction>> GetByUserIDAsync(Guid userID);
    Task<IEnumerable<Transaction>> GetAllTransactionsForSummaryAsync(bool userSummary = false);
  }
}
