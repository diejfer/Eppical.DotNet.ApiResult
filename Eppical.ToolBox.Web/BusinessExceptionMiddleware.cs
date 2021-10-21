using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Eppical.ToolBox.Web
{
    public class BusinessExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public BusinessExceptionMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
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
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {

            var apiResult = new ApiResult<string>(ex);
            if (ex is BusinessException)
            {
                var bex = (BusinessException)ex;
                if (bex.HttpStatusCode.HasValue)
                    apiResult.HttpCode = (int)bex.HttpStatusCode.Value;
                if (bex.StatusCode.HasValue)
                    apiResult.Code = bex.StatusCode.Value;
                else
                    apiResult.Code = 500;

                if (bex.HttpStatusCode != null)
                {
                    context.Response.StatusCode = (int)bex.HttpStatusCode;

                }
                else
                {
                    context.Response.StatusCode = 500;
                }

            }

            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                _logger.LogError("Unhandled exception", ex);
            }
            context.Response.ContentType = "application/json";



            await context.Response.WriteAsync(JsonSerializer.Serialize(apiResult));
        }
    }
}
