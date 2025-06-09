using System.Net;
using System.Text.Json;
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
                var res = new ApiResponse<string>(
                    Code: context.Response.StatusCode,
                    Data: null,
                    Message: "Resource not found",
                    Success: false
                );

                var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(res, option);

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
            }
        }
    }
}
