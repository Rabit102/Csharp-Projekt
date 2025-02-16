var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.UseDefaultFiles();
app.UseStaticFiles();


app.MapGet("/", () =>
{
    return Results.Redirect("/index.html");
});

app.Run();
