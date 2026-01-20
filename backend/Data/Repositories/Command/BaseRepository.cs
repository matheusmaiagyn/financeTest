using Data.EntityFramework.Data;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Command
{
  public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
  {
    protected readonly FinanceContext _db;

    public BaseRepository(FinanceContext db)
    {
      _db = db;
    }

    public async Task<TEntity> SaveOrUpdateAsync(TEntity obj)
    {
      // Busca a chave primaria do objeto
      var keyProperty = _db.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties[0];
      var keyValue = keyProperty.PropertyInfo.GetValue(obj);

      // Verifica se o objeto já existe no banco de dados
      var data = _db.Set<TEntity>().Find(keyValue);
      if (data != null)
      {
        _db.Entry(data).CurrentValues.SetValues(obj);
        await UpdateAsync(data);
      }
      else
      {
        _db.Set<TEntity>().Add(obj);
      }

      return obj;
    }

    private async Task UpdateAsync(TEntity obj)
    {
      // Realiza o update e seta o objeto como modificado
      var entry = _db.Entry(obj);

      if (entry.State == EntityState.Detached)
      {
        _db.Set<TEntity>().Attach(obj);
      }

      entry.State = EntityState.Modified;
    }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
      //retornando um objeto
      return await _db.Set<TEntity>().FindAsync(id);//find busca pela chave primaria (ID)
    }

    public async Task SaveChangesAsync()
    {
      await _db.SaveChangesAsync(); // Salva as mudanças no banco de dados
    }

    public async Task DeleteAsync(TEntity obj)
    {
      //remove o objeto
      _db.Set<TEntity>().Remove(obj);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
      //retornando uma lista de todos os objetos
      return await _db.Set<TEntity>().AsNoTracking().ToListAsync();
    }
  }
}
