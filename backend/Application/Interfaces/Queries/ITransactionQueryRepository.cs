using Application.DTOs.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Queries
{
  public interface ITransactionQueryRepository
  {
    Task<IEnumerable<TransactionResponseModel>> GetAllTransactionsAsync();
    Task<IEnumerable<TransactionResponseModel>> GetTransactionsByUserIDAsync(Guid userID);
    Task<TransactionsCategorySummaryResponseModel> GetTransactionSummaryForCategoriesAsync();
    Task<TransactionsUserSummaryResponseModel> GetTransactionSummaryForUsersAsync();
  }
}
