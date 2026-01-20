using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces.Services
{
  public interface IBaseService<TEntity> where TEntity : class
  {
    Task CreateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task<TEntity> GetByIDAsync(Guid id);
  }
}
