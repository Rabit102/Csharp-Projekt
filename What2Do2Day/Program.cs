var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddHttpClient("AniList", client =>
{
    client.BaseAddress = new Uri("https://graphql.anilist.co/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
builder.Services.AddDistributedMemoryCache(); // Für Session
builder.Services.AddSession(options => // Session aktivieren
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession(); // Session-Middleware hinzufügen
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.Run();