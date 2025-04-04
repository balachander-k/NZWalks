using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NZWalks.API.MiddleWares
{
    public class ExceptioinHandlerMiddleware
    {
        private readonly ILogger<ExceptioinHandlerMiddleware> _logger;
        private readonly RequestDelegate next;
        public ExceptioinHandlerMiddleware(ILogger<ExceptioinHandlerMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();

                //Log This Exception
                _logger.LogError(ex, $"{errorId}: {ex.Message}");


                //Return A Custom Exception
                httpContext.Response.StatusCode=(int) HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType= "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something Went Wrong! We are looking into resolving this"
            };

                await httpContext.Response.WriteAsJsonAsync(error);


            }
        }

    }
}
