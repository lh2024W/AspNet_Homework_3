using System.Diagnostics.Eventing.Reader;

namespace AspNet_Homework_3.Middleware
{
    public class FreeRoutingMiddleware
    {
        readonly IEnumerable<Book> books;
        readonly RequestDelegate next;

        public FreeRoutingMiddleware(RequestDelegate next, IEnumerable<Book> books)
        {
            this.books = books;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string path = context.Request.Path;
            if (path == "/")
            {
                context.Response.WriteAsync("Welcome on Book Api!");
            }
            else if (path == "/allbooks")
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync(Helper.GenerateHtmlPage(
                    Helper.BuildHtmlTable(books), "All Books"));
            }
            else 
            {
                await next.Invoke(context);
            }

        }
    }
}
