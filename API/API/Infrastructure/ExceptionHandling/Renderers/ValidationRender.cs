using System;
using System.Net;
using System.Threading.Tasks;
using Domain.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace API.Infrastructure.ExceptionHandling.Renderers
{
    [Boilerplate]
    public class ValidationRender : IExceptionRender
    {
        public bool ShouldHandle(Exception exception)
        {
            return exception is CommandValidationException;
        }

        public Task Render(HttpContext context, Exception exception)
        {
            var notValid = (CommandValidationException)exception;
            var code = HttpStatusCode.BadRequest;
            var result = JsonConvert.SerializeObject(new { errors = notValid.ValidationFailures });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
