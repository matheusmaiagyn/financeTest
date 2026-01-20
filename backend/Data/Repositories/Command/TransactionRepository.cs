using Data.EntityFramework.Data;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Command
{
  public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
  {
    public TransactionRepository(FinanceContext db) 
      : base(db)
    {
    }

    public async Task<IEnumerable<Transaction>> GetAllTransactionsForSummaryAsync(bool userSummary = false)
    {
      var query = _db.Set<Transaction>().AsNoTracking();

      if (userSummary)
        query = query.Include(t => t.User);
      else
        query = query.Include(t => t.Category);

      return await query.ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> GetByUserIDAsync(Guid userID)
    {
      var result =  _db.Set<Transaction>().AsNoTracking().AsQueryable().Where(t => t.UserID == userID);
      return await result.ToListAsync();
    }
  }
}
