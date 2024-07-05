using System.Net;
using System.Text.Json;
using Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Controllers;

public class GlobalExceptionHandlingMiddleaware
{

    private readonly RequestDelegate _next;

    public GlobalExceptionHandlingMiddleaware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(httpContext, exception);
        }
    }

    private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        HttpStatusCode statusCode;
        var stackTrace = string.Empty;
        string message = "";
        
        message = exception.Message;
        statusCode = HttpStatusCode.InternalServerError;
        stackTrace = exception.StackTrace;
        

        var exceptionResult = JsonSerializer.Serialize(new { error = message, stackTrace });
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)statusCode;

        return httpContext.Response.WriteAsync(exceptionResult);
    }
}
