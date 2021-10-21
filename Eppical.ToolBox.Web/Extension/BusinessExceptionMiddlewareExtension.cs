using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eppical.ToolBox.Web.Extension
{
    public static class BusinessExceptionMiddlewareExtension
    {
        public static void AddBusinessExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<BusinessExceptionMiddleware>();
        }
    }
}
