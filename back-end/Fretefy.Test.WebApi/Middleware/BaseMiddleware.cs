using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Fretefy.Test.WebApi.Middleware
{
    public abstract class BaseMiddleware
    {
        private readonly RequestDelegate _next;
        protected static readonly ILogger Logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger("ErrorHandlingMiddleware");
        public BaseMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public abstract (HttpStatusCode code, object message) GetResponse(Exception ex);
        public async Task Invoke(HttpContext context)
        {
            try 
            {
                
                await _next(context);
            }
            catch(Exception ex)
            {
                var response = context.Response;
            
                var (status, message) = GetResponse(ex);
                response.ContentType = "application/json";
                response.StatusCode = (int) status;
                var json = System.Text.Json.JsonSerializer.Serialize(message);
                await response.WriteAsync(json);
            }
        }
    }
}

