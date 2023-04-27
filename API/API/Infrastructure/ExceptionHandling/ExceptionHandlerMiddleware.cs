using API.Infrastructure.ExceptionHandling.Renderers;
using Newtonsoft.Json;
using System.Net;

namespace API.Infrastructure.ExceptionHandling
{
    public class ExceptionHandlerMiddleware
    {
        readonly RequestDelegate next;
        readonly IEnumerable<IExceptionRender> _renders;

        public ExceptionHandlerMiddleware(RequestDelegate next, IEnumerable<IExceptionRender> renders)
        {
            this.next = next;
            _renders = renders;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var render = _renders.FirstOrDefault(exceptionRender => exceptionRender.ShouldHandle(ex));
                if (render == null)
                {
                    await HandleExceptionAsync(context, ex);
                }
                else
                {
                    await render.Render(context, ex);
                }
            }
        }

        static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }

}
