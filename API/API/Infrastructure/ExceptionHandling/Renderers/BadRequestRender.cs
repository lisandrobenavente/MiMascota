using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using API.Infrastructure.ExceptionHandling.Exceptions;

namespace API.Infrastructure.ExceptionHandling.Renderers
{
    [Boilerplate]
    public class BadRequestRender : IExceptionRender
    {
        public bool ShouldHandle(Exception exception)
        {
            return exception is BadRequestException;
        }

        public Task Render(HttpContext context, Exception exception)
        {
            var result = JsonConvert.SerializeObject(new { innerException = exception.InnerException });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsync(result);
        }
    }
}
