using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EntityFramework.EntityConfig
{
  public class CategoryConfiguration : IEntityTypeConfiguration<Category>
  {
    public void Configure(EntityTypeBuilder<Category> builder)
    {
      builder.HasKey(u => u.ID);

      builder.Property(c => c.Description)
             .IsRequired()
             .HasMaxLength(200);

      builder.Property(c => c.CategoryType)
             .IsRequired();

      builder.HasIndex(u => u.CategoryType); // Index para o tipo de categoria
    }
  }
}
