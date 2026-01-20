using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Domain.Exceptions;

namespace Infra
{
  public sealed class ExceptionHandlingMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
      _next = next;
      _logger = logger;
    }

    public async Task Invoke(HttpContext ctx)
    {
      try
      {
        await _next(ctx);
      }
      catch (UnauthorizedException ex)
      {
        await WriteProblem(ctx, StatusCodes.Status401Unauthorized, ex.Message, ex);
      }
      catch (ValidationException ex)
      {
        var errors = ex.Errors
          .GroupBy(e => e.PropertyName)
          .ToDictionary(
            g => g.Key,
            g => g.Select(e => e.ErrorMessage).ToArray()
          );

        await WriteProblem(ctx, StatusCodes.Status422UnprocessableEntity, ex.Message, ex, "https://httpstatuses.com/422", errors);
      }
      catch (NotFoundException ex)
      {
        await WriteProblem(ctx, StatusCodes.Status404NotFound, ex.Message, ex);
      }
      catch (ArgumentException ex)
      {
        await WriteProblem(ctx, StatusCodes.Status400BadRequest, ex.Message, ex, "https://httpstatuses.com/400");
      }
      catch (Exception ex)
      {
        await WriteProblem(ctx, StatusCodes.Status500InternalServerError,
            "Ocorreu um erro inesperado.", ex, "https://httpstatuses.com/500");
      }
    }

    private async Task WriteProblem(HttpContext ctx, int status, string detail, Exception ex, string? type = null, IDictionary<string, string[]>? errors = null)
    {
      var problem = new ProblemDetails
      {
        Status = status,
        Title = ReasonPhrases.GetReasonPhrase(status),
        Detail = detail,
        Type = type,
        Instance = ctx.Request.Path
      };

      problem.Extensions["traceId"] = ctx.TraceIdentifier;

      // Se houver erros de validação, adiciona no extensions
      if (errors is not null && errors.Any())
      {
        problem.Extensions["errors"] = errors;
      }

      if (status >= 500)
        _logger.LogError(ex, "{Title}: {Detail}", problem.Title, detail);
      else
        _logger.LogWarning(ex, "{Title}: {Detail}", problem.Title, detail);

      ctx.Response.ContentType = "application/problem+json";
      ctx.Response.StatusCode = status;
      await ctx.Response.WriteAsJsonAsync(problem);
    }
  }
}
