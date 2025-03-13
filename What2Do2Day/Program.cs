var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddControllers(); // Add this line
builder.Services.AddHttpClient("AniList", client =>
{
    client.BaseAddress = new Uri("https://graphql.anilist.co/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
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
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers(); 
app.Run();