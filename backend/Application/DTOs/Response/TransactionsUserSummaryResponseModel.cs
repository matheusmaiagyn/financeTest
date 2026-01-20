
namespace Application.DTOs.Response
{
  public class TransactionsUserSummaryResponseModel
  {
    public UserSummaryResponseModel[] UserSummaries { get; set; } = Array.Empty<UserSummaryResponseModel>();
    public decimal TotalIncome { get; set; }
    public decimal TotalExpense { get; set; }
    public decimal NetBalance { get; set; }
  }

  public class UserSummaryResponseModel
  {
    public Guid UserID { get; set; }
    public string UserName { get; set; } = string.Empty;
    public decimal TotalIncome { get; set; }
    public decimal TotalExpense { get; set; }
    public decimal NetBalance { get; set; }
  }
}
