using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Interfaces.AppServices;
using Microsoft.AspNetCore.Mvc;

namespace FinanceControl.Controllers
{
  public class CategoryController : BaseController
  {
    private readonly ICategoryAppService _categoryAppService;

    public CategoryController(ICategoryAppService categoryAppService)
    {
      _categoryAppService = categoryAppService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddCategoryRequestModel model)
    {
      await _categoryAppService.CreateAsync(model);
      return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
      await _categoryAppService.DeleteAsync(id);
      return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
      var ret = await _categoryAppService.GetAllAsync();
      return Ok(ret);
    }
  }
}
