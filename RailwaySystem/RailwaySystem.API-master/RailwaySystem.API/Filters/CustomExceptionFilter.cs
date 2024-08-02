using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;

namespace RailwaySystem.API.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception;
            Console.WriteLine("************************************");
            Console.WriteLine(ex);
            Console.WriteLine("************************************");

            if (ex.InnerException?.Message.Contains("duplicate key", StringComparison.OrdinalIgnoreCase) == true)
            {
                context.Result = new JsonResult(new
                {
                    error = "Duplicate Record",
                    message = "Duplicate Record",
                    innerException = "",
                    stackTrace = ""
                })
                {
                    StatusCode = 500
                };
            }
            else if (ex.InnerException?.Message.Contains("The INSERT statement conflicted with the FOREIGN KEY constraint", StringComparison.OrdinalIgnoreCase) == true)
            {
                context.Result = new JsonResult(new
                {
                    error = "Id / Key not found!",
                    message = "Id / Key not found!",
                    innerException = "",
                    stackTrace = ""
                })
                {
                    StatusCode = 500
                };
            }
            else
            {
                context.Result = new JsonResult(new
                {
                    error = "Error occurred. Please try again.",
                    message = ex,
                    innerException = ex.InnerException?.Message,
                    stackTrace = ex.StackTrace
                })
                {
                    StatusCode = 500
                };
            }
            // Log the exception (you can use any logging framework you prefer)
            // For example, log to a file, database, or monitoring system
            /*
            // Create a custom response
            var response = new
            {
                Message = "An unexpected error occurred. Please try again later.",
                Detail = context.Exception.Message // Include this if you want to expose exception details
            };

            // Set the result
            context.Result = new JsonResult(response)
            {
                StatusCode = 500 // Internal Server Error
            };
            */
            context.ExceptionHandled = true; // Mark the exception as handled
        }
    }
}
