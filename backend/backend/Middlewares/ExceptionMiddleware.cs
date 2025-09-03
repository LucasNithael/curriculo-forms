using backend.Errors;
using System.Net;
using System.Text.Json;

namespace backend.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";

                // Definir o StatusCode com base no tipo de exceção
                context.Response.StatusCode = ex switch
                {
                    NaoAutorizadoException _ => (int)HttpStatusCode.Unauthorized, // 401 - Não autorizado
                    ProibidoException _ => (int)HttpStatusCode.Forbidden, // 403 - Proibido
                    NaoEncontradoException _ => (int)HttpStatusCode.NotFound, // 404 - Não encontrado
                    ConflitoException _ => (int)HttpStatusCode.Conflict, // 409 - Conflito
                    _ => (int)HttpStatusCode.InternalServerError // 500 - Erro interno do servidor
                };

                var response = _env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode.ToString(), ex.Message, ex.StackTrace)
                    : new ApiException(context.Response.StatusCode.ToString(), ex.Message, "Internal Server Error");

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
