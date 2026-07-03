using Dsw2026Ej15.Domain.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dsw2026Ej15.Api.Middlewares
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;


        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {

                await _next(context);
            }
            
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            if(ex is ValidationException) 
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            if(ex is NotFoundException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }

            return context.Response.WriteAsync(JsonSerializer.Serialize(new { Error = ex.Message}));
        }
    }
}
