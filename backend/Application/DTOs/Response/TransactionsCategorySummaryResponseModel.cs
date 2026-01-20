
namespace Application.DTOs.Response
{
  public class TransactionsCategorySummaryResponseModel
  {
    public CategorySummaryResponseModel[] CategorySummaries { get; set; } = Array.Empty<CategorySummaryResponseModel>();
    public decimal TotalIncome { get; set; }
    public decimal TotalExpense { get; set; }
    public decimal NetBalance { get; set; }
  }

  public class CategorySummaryResponseModel
  {
    public Guid CategoryID { get; set; }
    public string Description { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal TotalExpense { get; set; }
    public decimal NetBalance { get; set; }
  }
}
