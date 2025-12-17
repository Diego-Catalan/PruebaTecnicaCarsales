using System.Net;
using System.Text.Json;


namespace RickAndMortyBackend.Middleware
{
    // Middleware para manejar excepciones globalmente
    public class Middleware
    {
        private readonly RequestDelegate _next;

        public Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Error interno del servidor",
                Detail = exception.Message // Ocultar en producción
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
