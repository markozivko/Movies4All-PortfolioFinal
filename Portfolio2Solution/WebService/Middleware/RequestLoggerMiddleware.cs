using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataServiceLibrary;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WebService.Middleware
{

    public static class RequestLoggerMiddlewareExtension
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggerMiddleware>();
        }
    }

    public class RequestLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDataService _dataService;
        private ILogger _logger;

        public RequestLoggerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IDataService dataService)
        {
            _next = next;
            _dataService = dataService;
            _logger = loggerFactory.CreateLogger<RequestLoggerMiddleware>();
        }

        public async Task InvokeAsync(HttpContext context)
        {

            Program.CurrentUser = null;

            _logger.LogInformation(await FormatRequest(context.Request));

            var auth = context.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(auth))
            {
                Program.CurrentUser = _dataService.GetUser(Int32.Parse(auth.ToString()));
                Console.WriteLine($"========{Program.CurrentUser.UserId}");
            }


            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);

            // request.EnableBuffering(); is necessary for reading the Body stream more than once
            // You need to move the position back to the beginning, otherwise the next pice of middleware
            // that try to get the body will fail to do that, i.e. the position is in the end and thus the 
            // body seems to be empty
            request.Body.Position = 0;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }
    }
}
