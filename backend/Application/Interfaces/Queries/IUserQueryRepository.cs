using Application.DTOs.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Queries
{
  public interface IUserQueryRepository
  {
    Task<IEnumerable<UserResponseModel>> GetAllUsersAsync();
    Task<User?> GetByIDAsync(Guid id);
  }
}
