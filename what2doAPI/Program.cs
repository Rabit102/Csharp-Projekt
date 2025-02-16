var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Serve static files and configure the app to use a default route
app.UseDefaultFiles();
app.UseStaticFiles();

// Handle the root route to display the HTML content
app.MapGet("/", () =>
{
    return Results.Redirect("/index.html");
});

app.Run();
