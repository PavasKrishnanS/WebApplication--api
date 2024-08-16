using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using WebApplication_api; // Replace with your actual namespace

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    // Use exception handler for production environments
    app.UseExceptionHandler("/Error");
    // Enable HSTS for production environments
    app.UseHsts();
}

// Ensure HTTPS is used
app.UseHttpsRedirection();

// Serve static files (e.g., images, CSS, JS)
app.UseStaticFiles();

// Set up routing for endpoints
app.UseRouting();

// Enable authorization
app.UseAuthorization();

// Map Razor Pages endpoints
app.MapRazorPages();

app.Run();
