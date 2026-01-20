using Application.DTOs.Request.Models;
using Application.DTOs.Response;
using Application.Interfaces.AppServices;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceControl.Controllers
{
  public class UserController : BaseController
  {
    private readonly IUserAppService _userAppService;

    public UserController(IUserAppService userAppService)
    {
      _userAppService = userAppService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] AddUserRequestModel model)
    {
      await _userAppService.CreateAsync(model);
      return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
      await _userAppService.DeleteAsync(id);
      return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
      var ret = await _userAppService.GetAllAsync();
      return Ok(ret);
    }
  }
}
