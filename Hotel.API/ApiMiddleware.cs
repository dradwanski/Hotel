using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Hotel.API
{
    public class ApiMiddleware : IMiddleware
    {
        private readonly ILogger<ApiMiddleware> _logger;

        public ApiMiddleware(ILogger<ApiMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                if (e as AppException is not null)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync(e.Message);
                    _logger.LogWarning(e.Message);
                }
                else
                {
                    await context.Response.WriteAsync("Something went wrong");
                    _logger.LogError(e,e.Message);
                }
            }
        }
    }
}
