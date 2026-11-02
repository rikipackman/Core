namespace core.Midleware   
{
    public class storClosedMiddleware
    {
        private readonly RequestDelegate _next;
       private readonly IConfiguration _configuration;

        public storClosedMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
          _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var isStoreClosed = _configuration.GetValue<bool>("StoreSettings:IsStoreClosed");

            if (isStoreClosed)
            {
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                await context.Response.WriteAsync("The shop is currently closed. Please try again later.");
                return;
            }

            await _next(context);
        }
        
public static partial class MiddlewareExtensions
{
    public static IApplicationBuilder UsestorClosed(this IApplicationBuilder builder)
        => builder.UseMiddleware<storClosedMiddleware>();
}

    }
}


