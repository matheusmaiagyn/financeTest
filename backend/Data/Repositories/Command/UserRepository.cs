using Data.EntityFramework.Data;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Command
{
  public class UserRepository : BaseRepository<User>, IUserRepository
  {
    public UserRepository(FinanceContext db) 
      : base(db)
    {
    }
  }
}
