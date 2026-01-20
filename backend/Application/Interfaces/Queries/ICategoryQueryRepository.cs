using Application.DTOs.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Queries
{
  public interface ICategoryQueryRepository
  {
    Task<IEnumerable<CategoryResponseModel>> GetAllCategoriesAsync();
    Task<Category?> GetByIDAsync(Guid id);
  }
}
