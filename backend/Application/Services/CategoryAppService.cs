using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Interfaces.AppServices;
using Application.Interfaces.Queries;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
  public class CategoryAppService : ICategoryAppService
  {
    private readonly ICategoryService _categoryService;
    private readonly ICategoryQueryRepository _categoryQueryRepository;

    public CategoryAppService(ICategoryService categoryService, ICategoryQueryRepository categoryQueryRepository)
    {
      _categoryService = categoryService;
      _categoryQueryRepository = categoryQueryRepository;
    }

    public async Task CreateAsync(AddCategoryRequestModel model)
    {
      var category = Category.Create(model.Description, model.CategoryType);
      await _categoryService.CreateAsync(category);
    }

    public async Task DeleteAsync(Guid id)
    {
      var toDelete = await _categoryService.GetByIDAsync(id);
      await _categoryService.DeleteAsync(toDelete);
    }

    public async Task<IEnumerable<CategoryResponseModel>> GetAllAsync()
    {      
      return await _categoryQueryRepository.GetAllCategoriesAsync();
    }
  }
}
