using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using Alteration.Application.Infrastructure.Exceptions;

namespace Alteration.Api
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;

            switch (ex)
            {
                case ResourceNotFoundException _:
                    code = HttpStatusCode.NotFound;
                    break;
                case InvalidDomainOperationException _:
                    code = HttpStatusCode.BadRequest;
                    break;
            }

            var result = JsonConvert.SerializeObject(new { error = ex.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
