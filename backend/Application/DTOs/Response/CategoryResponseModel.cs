using Domain.Enums;

namespace Application.DTOs.Response
{
  public class CategoryResponseModel
  {
    public Guid ID { get; set; }
    public string Description { get; set; }
    public CategoryType CategoryType { get; set; }
  }
}
