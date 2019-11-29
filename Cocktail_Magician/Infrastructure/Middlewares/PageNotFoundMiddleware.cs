using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Cocktail_Magician.Infrastructure.Middlewares
{
    public class PageNotFoundMiddleware
    {
        private readonly RequestDelegate _next;

        public PageNotFoundMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await _next.Invoke(httpContext);
            if (httpContext.Response.StatusCode == 404)
            {
                httpContext.Response.Redirect("/Home/PageNotFoundFourOFour");
            }
        }
    
}
}
