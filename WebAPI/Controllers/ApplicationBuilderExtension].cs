using Microsoft.AspNetCore.Builder;

namespace WebAPI.Controllers;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
        => applicationBuilder.UseMiddleware<GlobalExceptionHandlingMiddleaware>();
}
