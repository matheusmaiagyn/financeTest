using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Interfaces.AppServices;
using Application.Interfaces.Queries;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Factories;
using Domain.Interfaces.Factories;
using Domain.Interfaces.Services;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
  public class TransactionAppService : ITransactionAppService
  {
    private readonly ITransactionService _transactionService;
    private readonly ITransactionFactory _transactionFactory;
    private readonly ITransactionQueryRepository _transactionQueryRepository;
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly ICategoryQueryRepository _categoryQueryRepository;

    public TransactionAppService(ITransactionService transactionService, ITransactionQueryRepository transactionQueryRepository, ITransactionFactory transactionFactory,
      IUserQueryRepository userQueryRepository, ICategoryQueryRepository categoryQueryRepository)
    {
      _transactionService = transactionService;
      _transactionQueryRepository = transactionQueryRepository;
      _transactionFactory = transactionFactory;
      _userQueryRepository = userQueryRepository;
      _categoryQueryRepository = categoryQueryRepository;
    }

    public async Task CreateAsync(AddTransactionRequestModel model)
    {
      var user = await _userQueryRepository.GetByIDAsync(model.UserID) ?? throw new NotFoundException("User");
      var category = await _categoryQueryRepository.GetByIDAsync(model.CategoryID) ?? throw new NotFoundException("Category");

      var transaction = _transactionFactory.Create(user, category, model.Description, model.Amount, model.TransactionType);
      await _transactionService.CreateAsync(transaction);
    }

    public async Task DeleteAsync(Guid id)
    {
      var toDelete = await _transactionService.GetByIDAsync(id);
      await _transactionService.DeleteAsync(toDelete);
    }

    public async Task<IEnumerable<TransactionResponseModel>> GetAllAsync()
    {
      return await _transactionQueryRepository.GetAllTransactionsAsync();
    }

    public async Task<IEnumerable<TransactionResponseModel>> GetTransactionsByUserIDAsync(Guid userID)
    {
      return await _transactionQueryRepository.GetTransactionsByUserIDAsync(userID);
    }

    public async Task<TransactionsCategorySummaryResponseModel> GetTransactionSummaryForCategories()
    {
      return await _transactionQueryRepository.GetTransactionSummaryForCategoriesAsync();
    }

    public async Task<TransactionsUserSummaryResponseModel> GetTransactionSummaryForUsers()
    {
      return await _transactionQueryRepository.GetTransactionSummaryForUsersAsync();
    }
  }
}
