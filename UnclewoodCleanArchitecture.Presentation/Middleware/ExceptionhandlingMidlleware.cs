
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UnclewoodCleanArchitecture.Application.Exceptions;
using UnclewoodCleanArchitecture.Domain.Exepptions;
using UnclewoodCleanArchitecture.Domain.Exepptions.Common;
using ValidationException = UnclewoodCleanArchitecture.Application.Exceptions.ValidationException;

namespace UnclewoodCleanArchitectur.Presentation.Middleware;

public class ExceptionhandlingMidlleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionhandlingMidlleware> _logger;


    public ExceptionhandlingMidlleware(RequestDelegate next, ILogger<ExceptionhandlingMidlleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception,"Exception occured {Message}", exception.Message);

            var exceptionDetail = GetExceptionDetails(exception);
            var problemDetails = new ProblemDetails
            {
                Status = exceptionDetail.Status,
                Type = exceptionDetail.Type,
                Title = exceptionDetail.Title,
                Detail = exceptionDetail.Detail,
            };
            if (exceptionDetail.Errors is not null)
            {
                problemDetails.Extensions.Add("errors" , exceptionDetail.Errors);
            }

            context.Response.StatusCode = exceptionDetail.Status;
            context.Response.ContentType = "application/json"; // Set content type
            await context.Response.WriteAsJsonAsync(problemDetails); // Use WriteAsJs
        }
        
    }

    private static ExceptionDetail GetExceptionDetails(Exception exception)
    {
        return exception switch
        {
            ValidationException validationException => new ExceptionDetail(
                StatusCodes.Status400BadRequest,
                "ValidationFailure",
                "Validation error",
                "One or more validation errors has occured",
                validationException.Errors
            ),
            DomainException domainException => new ExceptionDetail(
                StatusCodes.Status400BadRequest,
                "DomainFailure",
                "Domain error",
                "One or more domain errors has occured",
                domainException.Errors
            ),
            _ => new ExceptionDetail(
                StatusCodes.Status500InternalServerError,
                "ServerError",
                "Server error",
                "An unexpected error has occurred",
                null
            )
        };
    }
}
internal record ExceptionDetail(int Status, string Type,string Title, string Detail, IEnumerable<object>? Errors);