using AspNet_Homework_3.Middleware;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Book> books = new List<Book>()
{
    new Book("Book 1", "Music", 101),
    new Book("Book 2", "Music", 202),
    new Book("Book 3", "Music", 303),
    new Book("Book 4", "Music", 404),
    new Book("Book 5", "Music", 505)
};

//https://localhost:7040/allbooks
app.UseMiddleware<FreeRoutingMiddleware>(books);

//https://localhost:7040/getbooks?token=token12345&category=music
app.UseMiddleware<TokenMiddleware>("token12345", books);

app.Run(async (context) =>
{
    context.Response.StatusCode = 404;
    await context.Response.WriteAsync("Page Not Found!");
});

app.Run();

public record Book(string Name, string Category, decimal Price);