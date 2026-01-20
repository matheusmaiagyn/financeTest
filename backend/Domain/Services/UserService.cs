using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
  public class UserService : BaseService<User>, IUserService
  {
    private IUserRepository _userRepository => (IUserRepository)BaseRepository;

    public UserService(IUserRepository userRepository)
      : base(userRepository)
    {
    }
  }
}
