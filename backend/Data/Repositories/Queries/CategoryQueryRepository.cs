using Application.DTOs.Response;
using Application.Interfaces.Queries;
using Data.EntityFramework.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Queries
{
  public class CategoryQueryRepository : ICategoryQueryRepository
  {
    private readonly FinanceContext _db;

    public CategoryQueryRepository(FinanceContext context)
    {
      _db = context;
    }

    public async Task<IEnumerable<CategoryResponseModel>> GetAllCategoriesAsync()
    {
      var entity = await _db.Set<Category>().AsNoTracking().ToListAsync();
      return entity.Select(e => new CategoryResponseModel()
      {
        ID = e.ID,
        Description = e.Description,
        CategoryType = e.CategoryType
      });
    }

    public Task<Category?> GetByIDAsync(Guid id)
    {
      return _db.Set<Category>().AsNoTracking().FirstOrDefaultAsync(c => c.ID == id);
    }
  }
}
