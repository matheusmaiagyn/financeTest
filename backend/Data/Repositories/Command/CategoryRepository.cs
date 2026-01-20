using Data.EntityFramework.Data;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Command
{
  public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
  {
    public CategoryRepository(FinanceContext db) 
      : base(db)
    {
    }
  }
}
