namespace API.Infrastructure.ExceptionHandling.Renderers
{
    public interface IExceptionRender
    {
        bool ShouldHandle(Exception exception);
        Task Render(HttpContext context, Exception exception);
    }
}
