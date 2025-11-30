using BookTrack.Shared.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BookTrack.API.Middlewares;

public class GlobalExcepetionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExcepetionHandler> _logger;

    public GlobalExcepetionHandler(ILogger<GlobalExcepetionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        ProblemDetails problemDetails;

        if (exception is NotFoundException notFoundEx)
        {
            problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Resource Not Found",
                Detail = notFoundEx.Message,
                Instance = httpContext.Request.Path
            };
        }else if (exception is IsbnAlreadyExistsException isbnAlreadyExistsEx)
        {
            problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Isbn already exists",
                Detail = isbnAlreadyExistsEx.Message,
                Instance = httpContext.Request.Path
            };
        }else if (exception is IdNotFoundOnInsertReviewException idNotFoundEx)
        {
            problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "user id or book id not found",
                Detail = idNotFoundEx.Message,
                Instance = httpContext.Request.Path
            };
        }else if (exception is ReviewAlreadyExistsException reviewAlreadyExistsEx)
        {
            problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "User already have a review for this book",
                Detail = reviewAlreadyExistsEx.Message,
                Instance = httpContext.Request.Path
            };
        }
        else
        {
            problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "An unexpected error occurred.",
                Instance = httpContext.Request.Path
            };
        }

        httpContext.Response.StatusCode = problemDetails.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}