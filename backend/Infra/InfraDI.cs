using Application.DTOs.Request.Validators;
using Application.Interfaces.AppServices;
using Application.Interfaces.Queries;
using Application.Services;
using Data.EntityFramework.Data;
using Data.Repositories.Command;
using Data.Repositories.Queries;
using Domain.Factories;
using Domain.Interfaces.Factories;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra
{
  public class InfraDI
  {
    private static void RegisterServices(IServiceCollection services)
    {
      // Register services
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<ICategoryService, CategoryService>();
      services.AddScoped<ITransactionService, TransactionService>();
    }

    private static void RegisterRepositories(IServiceCollection services)
    {
      // Register repositories
      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<ICategoryRepository, CategoryRepository>();
      services.AddScoped<ITransactionRepository, TransactionRepository>();
    }

    private static void RegisterQueryRepositories(IServiceCollection services)
    {
      // Register query repositories
      services.AddScoped<IUserQueryRepository, UserQueryRepository>();
      services.AddScoped<ICategoryQueryRepository, CategoryQueryRepository>();
      services.AddScoped<ITransactionQueryRepository, TransactionQueryRepository>();
    }

    private static void RegisterAppServices(IServiceCollection services)
    {
      services.AddScoped<IUserAppService, UserAppService>();
      services.AddScoped<ICategoryAppService, CategoryAppService>();
      services.AddScoped<ITransactionAppService, TransactionAppService>();
    }

    private static void RegisterValidators(IServiceCollection services)
    {
      services.AddValidatorsFromAssemblyContaining<AddCategoryRequestModelValidator>();
      services.AddValidatorsFromAssemblyContaining<AddTransactionRequestModelValidator>();
      services.AddValidatorsFromAssemblyContaining<AddUserRequestModelValidator>();
    }

    private static void RegisterFactories(IServiceCollection services)
    {
      services.AddScoped<ITransactionFactory, TransactionFactory>();
    }

    public static void Register(IServiceCollection services)
    {
      RegisterDbContext(services);
      RegisterServices(services);
      RegisterRepositories(services);
      RegisterQueryRepositories(services);
      RegisterAppServices(services);
      RegisterValidators(services);
      RegisterFactories(services);
    }

    private static void RegisterDbContext(IServiceCollection services)
    {
      services.AddDbContext<FinanceContext>();
    }

    public static string ConfigureCORS(IServiceCollection services)
    {
      var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
      services.AddCors(options =>
      {
        options.AddPolicy(name: MyAllowSpecificOrigins,
                          policy =>
                          {
                            policy.WithOrigins("http://localhost:5173")
                            .AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                          });
      });

      return MyAllowSpecificOrigins;
    }
  }
}
