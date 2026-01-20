using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EntityFramework.EntityConfig
{
  public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
  {
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
      builder.HasKey(t => t.ID);

      builder.Property(t => t.Description)
             .IsRequired()
             .HasMaxLength(500);

      builder.Property(t => t.Amount)
             .IsRequired()
             .HasColumnType("decimal(18,2)");

      builder.Property(t => t.TransactionType)
             .IsRequired();

      builder.HasIndex(t => t.TransactionType); // Index para o tipo de transação

      // Configuração do relacionamento com Category
      builder.HasOne(t => t.Category)
             .WithMany()
             .HasForeignKey(t => t.CategoryID)
             .OnDelete(DeleteBehavior.Restrict);

      // Configuração do relacionamento com User
      builder.HasOne(t => t.User)
             .WithMany()
             .HasForeignKey(t => t.UserID)
             .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
