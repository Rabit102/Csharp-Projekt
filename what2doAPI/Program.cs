var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.UseDefaultFiles();
app.UseStaticFiles();


app.MapGet("/", () =>
{
    return Results.Redirect("/index.html");
});

app.MapGet("/anime", () =>
{
    return Results.Redirect("/IndexAnime.html");
});

app.MapGet("/read", () =>
{
    return Results.Redirect("/IndexRead.html");
});

app.Run();
