namespace core.Midleware   
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                
                 if (context.Response.HasStarted)
                  {
                    throw;
                  }
                 context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status500InternalServerError  ;
             context.Response.ContentType = "application/json; charset=utf-8";
                var errorResponse = new  { error = "An unexpected error occurred please try again later .",    status = 500};
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }

    public static partial class MiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
            => builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}