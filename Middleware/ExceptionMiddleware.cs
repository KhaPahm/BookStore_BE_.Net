using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using BookStore.Exceptions;
using BookStore.Models.ResponeApi;

namespace BookStore.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
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
            catch (Exception e)
            {
                _logger.LogError(e, "Unhandled exception occured");

                var statusCode = e switch
                {
                    NotFoundException => (int)HttpStatusCode.NotFound,
                    BadRequestException => (int)HttpStatusCode.BadRequest,
                    ConflictException => (int)HttpStatusCode.Conflict,
                    ForbiddenException => (int)HttpStatusCode.Forbidden,
                    UnauthorizedException => (int)HttpStatusCode.Unauthorized,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                var res = new ApiResponse<object>(
                    Code: statusCode,
                    Data: _env.IsDevelopment() ? e.StackTrace : null,
                    Message:  e.Message,
                    Success: false
                );

                var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;

                var json = JsonSerializer.Serialize(res, option);
                await context.Response.WriteAsync(json);
            }
        }
    }
}