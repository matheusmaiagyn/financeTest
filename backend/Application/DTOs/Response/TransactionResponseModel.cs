using Domain.Enums;

namespace Application.DTOs.Response
{
  public class TransactionResponseModel
  {
    public Guid ID { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public TransactionType TransactionType { get; set; }
    public Guid CategoryID { get; set; }
    public string CategoryDescription { get; set; }
    public Guid UserID {  get; set; }
    public string UserName { get; set; }
  }
}
