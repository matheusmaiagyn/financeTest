using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
  public class CategoryService : BaseService<Category>, ICategoryService
  {
    private ICategoryRepository _transactionRepository => (ICategoryRepository)BaseRepository;

    public CategoryService(ICategoryRepository categoryRepository)
      : base(categoryRepository) 
    {
    }
  }
}
