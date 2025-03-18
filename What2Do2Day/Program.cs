var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddRazorPages(); // Adds Razor Pages support
builder.Services.AddControllers(); // Adds API controller support
builder.Services.AddHttpClient("AniList", client => // Configures HTTP client for AniList API
{
    client.BaseAddress = new Uri("https://graphql.anilist.co/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
builder.Services.AddDistributedMemoryCache(); // Enables in-memory caching for sessions
builder.Services.AddSession(options => // Configures session settings
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session expires after 30 min
    options.Cookie.HttpOnly = true; // Prevents JS access to session cookie
    options.Cookie.IsEssential = true; // Marks cookie as essential for GDPR
});

var app = builder.Build();

// Middleware pipeline
if (!app.Environment.IsDevelopment()) // Production-only middleware
{
    app.UseExceptionHandler("/Error"); // Custom error page
    app.UseHsts(); // Enforces HTTPS
}
app.UseHttpsRedirection(); // Redirects HTTP to HTTPS
app.UseStaticFiles(); // Serves static files (CSS, JS, etc.)
app.UseRouting(); // Enables routing
app.UseSession(); // Enables session middleware
app.UseAuthorization(); // Adds authorization checks
app.MapRazorPages(); // Maps Razor Pages routes
app.MapControllers(); // Maps API controller routes
app.Run(); // Starts the application