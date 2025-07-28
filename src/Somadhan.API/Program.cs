using CorrelationId;
using CorrelationId.DependencyInjection;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;

using Serilog;

using Somadhan.API.Extentions;
using Somadhan.API.Seed;
using Somadhan.Application.Common;
using Somadhan.Application.Common.Handlers;
using Somadhan.Application.Dtos;
using Somadhan.Domain.Modules.Product;
using Somadhan.Infrastructure.Data;
using Somadhan.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341") // Optional: change for production
    .CreateLogger();

builder.Host.UseSerilog();

// --- Service registrations via extensions ---
builder.Services
    .AddAppDbContext(builder.Configuration)
    .AddAppIdentity()
    .AddRepositories()
    .AddMediatorAndValidators()
    .AddJwtAuthentication(builder.Configuration)
    .AddApiServices()
    .AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    })
    .AddVersionedApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    })
    .AddSwaggerDocumentation();

builder.Services.AddDefaultCorrelationId();

var app = builder.Build();

// --- Data Seeding ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = services.GetRequiredService<AppDbContext>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await DataSeeder.SeedAsync(db, userManager, roleManager);
}

// --- Middleware pipeline ---
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    // c.SwaggerEndpoint("/swagger/v1/swagger.json", "Somadhan API"); // This label appears in the dropdown
    // c.DocumentTitle = "Somadhan API Docs"; // Optional - browser tab title
    // c.RoutePrefix = ""; // Optional - Swagger UI at root (localhost:5000/)
});
app.UseAppMiddlewares();
app.MapControllers();

app.UseResponseCaching();
app.UseCorrelationId();

app.MapGet("/", async context =>
{
    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync(@"
        <!DOCTYPE html>
        <html lang=""en"">
        <head>
            <meta charset=""UTF-8"">
            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
            <title>Inventory Service API</title>
            <style>
                body {
                    font-family: 'Segoe UI', Arial, sans-serif;
                    background: #f8fafc;
                    color: #1a202c;
                    margin: 0;
                    padding: 0;
                }
                .container {
                    max-width: 600px;
                    margin: 60px auto;
                    background: #fff;
                    box-shadow: 0 2px 16px rgba(0,0,0,0.08);
                    border-radius: 10px;
                    padding: 40px 30px;
                    text-align: center;
                }
                h1 {
                    color: #2563eb;
                    margin-bottom: 10px;
                }
                p {
                    margin: 20px 0;
                }
                a.button {
                    display: inline-block;
                    margin-top: 18px;
                    background: #2563eb;
                    color: #fff;
                    text-decoration: none;
                    padding: 12px 28px;
                    border-radius: 6px;
                    font-weight: 600;
                    transition: background 0.2s;
                }
                a.button:hover {
                    background: #1e40af;
                }
                .footer {
                    margin-top: 30px;
                    font-size: 0.95em;
                    color: #64748b;
                }
            </style>
        </head>
        <body>
            <div class=""container"">
                <h1>Inventory Service API</h1>
                <p>🚀 Welcome to the Inventory Service API landing page.</p>
                <p>
                    <a class=""button"" href=""/swagger"">View API Documentation</a>
                </p>
                <div class=""footer"">
                    &copy; " + DateTime.Now.Year + @" Inventory Service • <a href=""https://yourdomain.example"" style=""color:#2563eb;text-decoration:underline;"">Visit our site</a>
                </div>
            </div>
        </body>
        </html>
    ");
});

app.Run();
