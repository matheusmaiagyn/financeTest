using Domain.Enums;

namespace Application.DTOs.Request
{
  public class AddCategoryRequestModel
  {
    public string Description { get; set; }
    public CategoryType CategoryType { get; set; }
  }
}
