using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EntityFramework.EntityConfig
{
  public class UserConfiguration : IEntityTypeConfiguration<User>
  {
    public void Configure(EntityTypeBuilder<User> builder)
    {
      builder.HasKey(u => u.ID);

      builder.Property(u => u.Name)
             .IsRequired()
             .HasMaxLength(100);

      builder.Property(u => u.Age)
             .IsRequired();

      builder.HasIndex(u => u.Age); // Index idade para performance em consultas
    }
  }
}
