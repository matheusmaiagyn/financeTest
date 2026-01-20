using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
  public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
  {
    protected readonly IBaseRepository<TEntity> BaseRepository;

    public BaseService(IBaseRepository<TEntity> baseRepository)
    {
      BaseRepository = baseRepository;
    }

    public async Task CreateAsync(TEntity entity)
    {
      await BaseRepository.SaveOrUpdateAsync(entity);
      await BaseRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
      await BaseRepository.DeleteAsync(entity);
      await BaseRepository.SaveChangesAsync();
    }

    public async Task<TEntity> GetByIDAsync(Guid id)
    {
      return await BaseRepository.GetByIdAsync(id);
    }
  }
}
