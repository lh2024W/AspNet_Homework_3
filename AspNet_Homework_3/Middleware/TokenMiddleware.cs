using System.Diagnostics.Eventing.Reader;
using System.Runtime.InteropServices;

namespace AspNet_Homework_3.Middleware
{
    public class TokenMiddleware
    {
        readonly IEnumerable<Book> books;
        private readonly RequestDelegate next;
        string pattern;

        public TokenMiddleware(RequestDelegate next, string pattern, IEnumerable<Book> books)
        {
            this.next = next;
            this.pattern = pattern;
            this.books = books;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == "/getbooks")
            {
                var token = context.Request.Query["token"];
                if (token != pattern)
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("Token is invalid");
                }
                else
                {
                    var bookCategory = context.Request.Query["category"];
                    context.Response.ContentType = "text/html; charset =utf-8";
                    await context.Response.WriteAsync(Helper.GenerateHtmlPage(
                        Helper.BuildHtmlTable(books.Where(e => e.Category.Equals(bookCategory, StringComparison.OrdinalIgnoreCase))),
                        $"Category: {bookCategory}"
                        ));
                }
            }
            else
            {
                await next.Invoke(context);
            }
        }
    }
}
