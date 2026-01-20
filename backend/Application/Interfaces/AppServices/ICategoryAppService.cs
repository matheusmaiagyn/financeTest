using Application.DTOs.Request;
using Application.DTOs.Response;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.AppServices
{
  public interface ICategoryAppService
  {
    Task CreateAsync(AddCategoryRequestModel model);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<CategoryResponseModel>> GetAllAsync();
  }
}
