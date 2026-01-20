using Domain.Enums;

namespace Application.DTOs.Request
{
  public class AddTransactionRequestModel
  {
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public TransactionType TransactionType { get; set; }
    public Guid CategoryID { get; set; }
    public Guid UserID { get; set; }
  }
}
