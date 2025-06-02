using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// httpClient for API communication
builder.Services.AddHttpClient();

// Add session services
builder.Services.AddDistributedMemoryCache(); 
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
    // Cookie settings
    options.Cookie.HttpOnly = true; 
    // Make the session cookie essential
    options.Cookie.IsEssential = true; 
});

var app = builder.Build();

// Configure the HTTP request pipeline.

// Enable static files (e.g., wwwroot)
app.UseStaticFiles();

// Enable routing
app.UseRouting();

// Enable session
app.UseSession();
app.UseAuthentication(); 
app.UseAuthorization();

// Map controller routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
