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
  public class UserQueryRepository : IUserQueryRepository
  {
    private readonly FinanceContext _db;

    public UserQueryRepository(FinanceContext context)
    {
      _db = context;
    }

    public async Task<IEnumerable<UserResponseModel>> GetAllUsersAsync()
    {
      var entity = await _db.Set<User>().AsNoTracking().ToListAsync();
      return entity.Select(e => new UserResponseModel()
      {
        ID = e.ID,
        Name = e.Name,
        Age = e.Age
      });
    }

    public Task<User?> GetByIDAsync(Guid id)
    {
      return _db.Set<User>().AsNoTracking().FirstOrDefaultAsync(u => u.ID == id);
    }
  }
}
