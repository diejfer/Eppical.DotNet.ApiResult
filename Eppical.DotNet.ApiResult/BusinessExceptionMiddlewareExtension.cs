using Microsoft.AspNetCore.Builder;

namespace Eppical.DotNet.ApiResult
{
    public static class BusinessExceptionMiddlewareExtension
    {
        public static void AddBusinessExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<BusinessExceptionMiddleware>();
        }
    }

}
