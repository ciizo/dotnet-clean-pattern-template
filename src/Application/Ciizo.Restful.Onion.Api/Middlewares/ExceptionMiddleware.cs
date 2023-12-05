using Ciizo.Restful.Onion.Api.Middlewares.Models;
using FluentValidation;
using System.Net;
using System.Text;

namespace Ciizo.Restful.Onion.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
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

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)(exception switch
            {
                UnauthorizedAccessException => HttpStatusCode.Forbidden,
                ArgumentException => HttpStatusCode.BadRequest,
                ValidationException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError,
            });

            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString(), Encoding.UTF8);
        }
    }
}