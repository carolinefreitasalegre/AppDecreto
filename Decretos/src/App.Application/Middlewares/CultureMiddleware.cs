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
        var cultureHeader = context.Request.Headers["Accept-Language"].ToString();

        var culture = cultureHeader?
            .Split(',')
            .FirstOrDefault()?
            .Split(';')  
            .FirstOrDefault()?
            .Trim();

        if (string.IsNullOrWhiteSpace(culture))
            culture = "pt-BR";

        var cultureInfo = new CultureInfo(culture);

        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;

        await _next(context);
    }
}