using Application.DTOs.Request;
using Application.DTOs.Request.Models;
using Application.DTOs.Response;
using Application.Interfaces.AppServices;
using Application.Interfaces.Queries;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Services;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
  public class UserAppService : IUserAppService
  {
    private readonly IUserService _userService;
    private readonly IUserQueryRepository _userQueryRepository;

    public UserAppService(IUserService userService, IUserQueryRepository userQueryRepository)
    {
      _userService = userService;
      _userQueryRepository = userQueryRepository;
    }

    public async Task CreateAsync(AddUserRequestModel model)
    {
      var user = User.Create(model.Name, model.Age);
      await _userService.CreateAsync(user);
    }

    public async Task DeleteAsync(Guid id)
    {
      var toDelete = await _userService.GetByIDAsync(id);
      await _userService.DeleteAsync(toDelete);
    }

    public async Task<IEnumerable<UserResponseModel>> GetAllAsync()
    {
      return await _userQueryRepository.GetAllUsersAsync();
    }
  }
}
