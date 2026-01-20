using Application.DTOs.Response;
using Application.Interfaces.Queries;
using Data.EntityFramework.Data;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Queries
{
  public class TransactionQueryRepository : ITransactionQueryRepository
  {
    private readonly FinanceContext _db;

    public TransactionQueryRepository(FinanceContext context)
    {
      _db = context;
    }

    public async Task<IEnumerable<TransactionResponseModel>> GetAllTransactionsAsync()
    {
      return await _db.Set<Transaction>()
        .AsNoTracking()
        .Select(e => new TransactionResponseModel()
          {
            ID = e.ID,
            Description = e.Description,
            Amount = e.Amount,
            TransactionType = e.TransactionType,
            CategoryID = e.CategoryID,
            CategoryDescription = e.Category.Description,
            UserID = e.UserID,
            UserName = e.User.Name
          })
        .ToListAsync();
    }

    public async Task<IEnumerable<TransactionResponseModel>> GetTransactionsByUserIDAsync(Guid userID)
    {
      return await _db.Set<Transaction>()
        .AsNoTracking()
        .Where(t => t.UserID == userID)
        .Select(e => new TransactionResponseModel()
          {
            ID = e.ID,
            Description = e.Description,
            Amount = e.Amount,
            TransactionType = e.TransactionType,
            CategoryID = e.CategoryID,
            CategoryDescription = e.Category.Description,
            UserID = e.UserID,
            UserName = e.User.Name
          })
        .ToListAsync();
    }

    public async Task<TransactionsCategorySummaryResponseModel> GetTransactionSummaryForCategoriesAsync()
    {
      var items = await _db.Set<Category>() // Inicia pela categoria para listar todas primeiro, depois busca transações
        .AsNoTracking()
        // Faz join com Transactions para buscar totais mesmo quando não há transações para a categoria.
        .GroupJoin(
            _db.Set<Transaction>().AsNoTracking(),
            c => c.ID,
            t => t.CategoryID,
            (c, txs) => new CategorySummaryResponseModel
            {
              CategoryID = c.ID,
              Description = c.Description,

              TotalIncome = txs
                    .Where(t => t.TransactionType == TransactionType.Income)
                    .Sum(t => (decimal?)t.Amount) ?? 0m,

              TotalExpense = txs
                    .Where(t => t.TransactionType == TransactionType.Expense)
                    .Sum(t => (decimal?)t.Amount) ?? 0m
            }
        )
        .ToListAsync();

      foreach (var i in items)
        i.NetBalance = i.TotalIncome - i.TotalExpense; // Calculo do saldo

      var result = new TransactionsCategorySummaryResponseModel
      {
        CategorySummaries = items.ToArray(),
        TotalIncome = items.Sum(x => x.TotalIncome),
        TotalExpense = items.Sum(x => x.TotalExpense),
        NetBalance = items.Sum(x => x.TotalIncome) - items.Sum(x => x.TotalExpense)
      };

      return result;
    }

    public async Task<TransactionsUserSummaryResponseModel> GetTransactionSummaryForUsersAsync()
    {
      var items = await _db.Set<User>() // Inicia pelos usuários para listar todos primeiro, depois busca transações
        .AsNoTracking()
        // Faz join com Transactions para buscar totais mesmo quando não há transações para o usuário.
        .GroupJoin(
            _db.Set<Transaction>().AsNoTracking(),
            u => u.ID,
            t => t.UserID,
            (u, txs) => new UserSummaryResponseModel
            {
              UserID = u.ID,
              UserName = u.Name,

              TotalIncome = txs
                    .Where(t => t.TransactionType == TransactionType.Income)
                    .Sum(t => (decimal?)t.Amount) ?? 0m,

              TotalExpense = txs
                    .Where(t => t.TransactionType == TransactionType.Expense)
                    .Sum(t => (decimal?)t.Amount) ?? 0m
            }
        )
        .ToListAsync();

      foreach (var i in items)
        i.NetBalance = i.TotalIncome - i.TotalExpense; // Calculo do saldo

      var result = new TransactionsUserSummaryResponseModel
      {
        UserSummaries = items.ToArray(),
        TotalIncome = items.Sum(x => x.TotalIncome),
        TotalExpense = items.Sum(x => x.TotalExpense),
        NetBalance = items.Sum(x => x.TotalIncome) - items.Sum(x => x.TotalExpense)
      };

      return result;
    }
  }
}
