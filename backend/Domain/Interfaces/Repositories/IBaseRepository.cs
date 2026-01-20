using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces.Repositories
{
  public interface IBaseRepository<TEntity> where TEntity : class
  {
    Task<TEntity> SaveOrUpdateAsync(TEntity obj);
    //seleciona por ID
    Task<TEntity> GetByIdAsync(Guid id);
    Task SaveChangesAsync();
    //realiza delete recebendo objeto
    Task DeleteAsync(TEntity obj);
    //retorna todos os objetos
    Task<IEnumerable<TEntity>> GetAllAsync();
  }
}
