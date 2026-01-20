using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Interfaces.AppServices;
using Microsoft.AspNetCore.Mvc;

namespace FinanceControl.Controllers
{
  public class TransactionController : BaseController
  {
    private readonly ITransactionAppService _transactionAppService;

    public TransactionController(ITransactionAppService transactionAppService)
    {
      _transactionAppService = transactionAppService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] AddTransactionRequestModel model)
    {
      await _transactionAppService.CreateAsync(model);
      return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTransactions()
    {
      var ret = await _transactionAppService.GetAllAsync();
      return Ok(ret);
    }

    [HttpGet("{userID}")]
    public async Task<IActionResult> GetTransactionsByUserID([FromQuery] Guid userID)
    {
      var ret = await _transactionAppService.GetTransactionsByUserIDAsync(userID);
      return Ok(ret);
    }

    [HttpGet("GetTransactionSummaryForUsers")]
    public async Task<IActionResult> GetTransactionSummaryForUsers()
    {
      var ret = await _transactionAppService.GetTransactionSummaryForUsers();
      return Ok(ret);
    }

    [HttpGet("GetTransactionSummaryForCategories")]
    public async Task<IActionResult> GetTransactionSummaryForCategories()
    {
      var ret = await _transactionAppService.GetTransactionSummaryForCategories();
      return Ok(ret);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTransaction(Guid id)
    {
      await _transactionAppService.DeleteAsync(id);
      return NoContent();
    }
  }
}
