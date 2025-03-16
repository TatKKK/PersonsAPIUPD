using Azure;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Runtime.CompilerServices;

namespace WebApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
           this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            logger.LogError(exception, exception.Message);

            object response;

            if (exception is DbUpdateException dbUpdateException)
            {
                response = new
                {
                    message = "Database update error. Please check foreign key constraints.",
                    error = dbUpdateException.InnerException?.Message
                };
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            // AddPerson-ze bad requestebs ar ichers
            else if (exception is ArgumentException || exception is InvalidOperationException || exception is BadHttpRequestException)
            {
                response = new { message = "Bad request. Please check your input.", error = exception.Message };
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                response = new { message = "An unexpected error occurred." };
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            context.Response.ContentType = "application/json";
            return context.Response.WriteAsJsonAsync(response);

        }
    }
}
