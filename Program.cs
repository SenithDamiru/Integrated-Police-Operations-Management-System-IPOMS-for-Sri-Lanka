using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Diagnostics;
using System.Text;
using testproj.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stripe;
using testproj.Models;
using testproj.Controllers;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Configure Stripe
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

// Read JWT settings from configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", corsBuilder =>
        corsBuilder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader());
});

// Add Controllers
builder.Services.AddControllers();

// Configure EF Core with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Set hosting URLs
builder.WebHost.UseUrls("http://0.0.0.0:5000", "https://0.0.0.0:5001");

var app = builder.Build();

// Enable static file middleware to serve files in wwwroot
app.UseStaticFiles(); // Serve static files (HTML, CSS, JS, images, etc.)

// Optionally set default file like home.html or index.html
app.UseDefaultFiles(new Microsoft.AspNetCore.Builder.DefaultFilesOptions
{
    DefaultFileNames = new System.Collections.Generic.List<string> { "home.html" }
});

// Enable CORS
app.UseCors("AllowAll");

// Routing middleware
app.UseRouting();

// Authentication and Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Global exception handler
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exception != null)
        {
            await context.Response.WriteAsJsonAsync(new { message = exception.Message });
        }
    });
});

// Enable Swagger UI in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();      // Serves the Swagger JSON document
    app.UseSwaggerUI();    // Serves the Swagger UI
}

// Map controllers to API endpoints
app.MapControllers();

// Run the application
app.Run();