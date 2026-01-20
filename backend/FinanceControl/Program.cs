using Application.DTOs.Request.Validators;
using FluentValidation;
using Infra;

namespace Server
{
  class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);
      // Add services to the container.
      builder.Services.AddControllers();
      // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
      builder.Services.AddOpenApi();
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      InfraDI.Register(builder.Services);
      var originCORS = InfraDI.ConfigureCORS(builder.Services);

      var app = builder.Build();
      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI(); // /swagger
      }
      app.UseHttpsRedirection();
      app.UseCors(originCORS);
      app.UseAuthorization();

      app.UseMiddleware<ExceptionHandlingMiddleware>();

      app.MapControllers();
      app.Run();
    }
  }
}
