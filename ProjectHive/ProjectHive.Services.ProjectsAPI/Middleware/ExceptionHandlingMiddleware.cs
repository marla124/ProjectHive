namespace ProjectHive.Services.ProjectsAPI.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при обработке запроса");

                httpContext.Response.StatusCode = 500;
                httpContext.Response.ContentType = "application/json";
                var errorDetails = new
                {
                    message = "Произошла ошибка на сервере. Пожалуйста, попробуйте позже.",
                    details = ex.Message
                };
                await httpContext.Response.WriteAsJsonAsync(errorDetails);
            }
        }
    }

}
