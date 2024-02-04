using Ciizo.CleanPattern.Domain.Business.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ciizo.CleanPattern.Api.Middlewares
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

            var error = new ValidationProblemDetails();

            if (exception is ValidationException)
            {
                foreach (var vex in ((ValidationException)exception).Errors)
                {
                    error.Errors.Add(vex.PropertyName, new[] { vex.ErrorMessage });
                }
            }
            else
            {
                error.Detail = exception.Message;
            }

            error.Status = (int)(exception switch
            {
                UnauthorizedAccessException => HttpStatusCode.Forbidden,
                ArgumentException => HttpStatusCode.BadRequest,
                ValidationException => HttpStatusCode.BadRequest,
                DataNotFoundException => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError,
            });

            await context.Response.WriteAsJsonAsync(error);
            //await context.Response.WriteAsync(new ErrorDetails()
            //{
            //    StatusCode = context.Response.StatusCode,
            //    Message = exception.Message
            //}.ToString(), Encoding.UTF8);
        }
    }
}