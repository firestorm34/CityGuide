global using CityGuide.Shared.DataAccess;
global using CityGuide.Shared.Models;
global using CityGuide.Shared.Hubs;
global using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR(o =>o.EnableDetailedErrors = true);
builder.Services.AddDbContext<CityGuideContext>(options => options.UseInMemoryDatabase("Cities"));
builder.Services.AddScoped<CityRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapHub<CityGuideHub>("/CityGuide");
app.MapFallbackToFile("Index.html");
app.Run();
