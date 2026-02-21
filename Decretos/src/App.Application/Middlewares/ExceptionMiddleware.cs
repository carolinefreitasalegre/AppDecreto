using System.Net;
using System.Text.Json;
using App.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace App.Application.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/jsno";

            var response = new
            {
                message = ex.Message
            };
            
            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
            
        }
    }
}