using IC_Adrovez.Infrastructure.Config;
using Microsoft.Extensions.Options;

namespace IC_Adrovez.Api.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApiKeySettings _apiKeySettings;

        public ApiKeyMiddleware(
            RequestDelegate next,
            IOptions<ApiKeySettings> apiKeyOptions)
        {
            _next = next;
            _apiKeySettings = apiKeyOptions.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Rutas públicas opcionales
            var path = context.Request.Path.Value;

            if (!string.IsNullOrWhiteSpace(path) &&
                (path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase)))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue(_apiKeySettings.HeaderName, out var extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new
                {
                    message = "ApiKey no enviada."
                });
                return;
            }

            if (string.IsNullOrWhiteSpace(_apiKeySettings.ApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new
                {
                    message = "La configuración de ApiKey no está definida."
                });
                return;
            }

            if (!_apiKeySettings.ApiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new
                {
                    message = "ApiKey inválida."
                });
                return;
            }

            await _next(context);
        }
    }
}

