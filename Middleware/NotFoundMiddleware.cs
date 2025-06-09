using System.Net;
using System.Text.Json;
using BookStore.Exceptions;
using BookStore.Models.ResponeApi;

namespace BookStore.Middleware
{
    public class NotFoundMiddleware
    {
        private readonly RequestDelegate _next;

        public NotFoundMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.NotFound && !context.Response.HasStarted)
            {
                throw new NotFoundException("Resource not found");
            }
        }
    }
}
