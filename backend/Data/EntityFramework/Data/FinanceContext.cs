using Data.EntityFramework.EntityConfig;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EntityFramework.Data
{
  public class FinanceContext : DbContext
  {
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;

    public FinanceContext(DbContextOptions<FinanceContext> options) 
      : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        try
        {
          optionsBuilder.UseSqlServer(
            GetConnection(),
            sql =>
            {
              sql.CommandTimeout(60); // segundos
              sql.EnableRetryOnFailure(
                  maxRetryCount: 6,
                  maxRetryDelay: TimeSpan.FromSeconds(15),
                  errorNumbersToAdd: new[] { 40613, 40197, 40501, 4060, 10928, 10929 }
              );
            }
          );
        }
        catch (Exception e)
        {
          throw new Exception(e.Message);
        }
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<User>().ToTable("Users");
      modelBuilder.Entity<Category>().ToTable("Categories");
      modelBuilder.Entity<Transaction>().ToTable("Transactions");

      modelBuilder.ApplyConfiguration(new UserConfiguration());
      modelBuilder.ApplyConfiguration(new CategoryConfiguration());
      modelBuilder.ApplyConfiguration(new TransactionConfiguration());
    }

    private string? GetConnection()
    {
      try
      {
        var builder = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        var configuration = builder.Build();
        var cnn = configuration.GetConnectionString("DbConnectionString");

        return cnn;
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }
  }
}
