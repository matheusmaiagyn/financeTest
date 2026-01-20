using Application.DTOs.Request;
using Application.DTOs.Request.Models;
using Application.DTOs.Response;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.AppServices
{
  public interface IUserAppService
  {
    Task CreateAsync(AddUserRequestModel model);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<UserResponseModel>> GetAllAsync();
  }
}
