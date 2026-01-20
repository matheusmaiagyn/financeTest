using Application.DTOs.Request;
using Application.DTOs.Response;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.AppServices
{
  public interface ITransactionAppService
  {
    Task CreateAsync(AddTransactionRequestModel model);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<TransactionResponseModel>> GetAllAsync();
    Task<IEnumerable<TransactionResponseModel>> GetTransactionsByUserIDAsync(Guid userID);
    Task<TransactionsUserSummaryResponseModel> GetTransactionSummaryForUsers();
    Task<TransactionsCategorySummaryResponseModel> GetTransactionSummaryForCategories();
  }
}
