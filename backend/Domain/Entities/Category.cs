using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
  public class Category
  {
    public Guid ID { get; private set; }
    public string Description { get; private set; }
    public CategoryType CategoryType { get; private set; }

    // For EF Core
    protected Category() { }

    private Category(string deescription, CategoryType categoryType)
    {
      SetDeescription(deescription);
      SetCategoryType(categoryType);
    }

    public static Category Create(string deescription, CategoryType categoryType)
    {
      return new Category(deescription, categoryType);
    }

    private void SetDeescription(string deescription)
    {
      Description = deescription;
    }

    private void SetCategoryType(CategoryType categoryType)
    {
      CategoryType = categoryType;
    }
  }
}
