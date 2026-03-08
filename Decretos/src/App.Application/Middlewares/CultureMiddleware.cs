using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace App.Application.Middlewares;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;

    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var requestCulture = context.Request.Headers["Accept-Language"].FirstOrDefault();
       
            CultureInfo.CurrentCulture = new CultureInfo(requestCulture);
            CultureInfo.CurrentUICulture = new CultureInfo(requestCulture);
        

            await _next(context);
    }
}