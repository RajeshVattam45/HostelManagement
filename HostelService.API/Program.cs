using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using HostelService.Infrastructure.Data;
using HostelService.Application.Services;
using HostelService.Domain.Interfaces;
using HostelService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder ( args );

// --------------------
// Load configuration in correct order
// --------------------
builder.Configuration
    .AddJsonFile ( "appsettings.json", optional: false, reloadOnChange: true )
    .AddJsonFile ( $"appsettings.{builder.Environment.EnvironmentName}.json", optional: true )
    .AddEnvironmentVariables ();

// --------------------
// Database Connection
// --------------------
var connectionString = builder.Configuration.GetConnectionString ( "DefaultConnection" );

builder.Services.AddDbContext<HostelDbContext> ( options =>
    options.UseSqlServer ( connectionString,
        b => b.MigrationsAssembly ( "HostelService.Infrastructure" ) )
);

// --------------------
// Dependency Injection
// --------------------
builder.Services.AddScoped<IHostelAppService, HostelAppService> ();
builder.Services.AddScoped<IHostelRepository, HostelRepository> ();

builder.Services.AddScoped<IRoomAppService, RoomAppService> ();
builder.Services.AddScoped<IRoomRepository, RoomRepository> ();

builder.Services.AddScoped<IHostelStudentRepository, HostelStudentRepository> ();
builder.Services.AddScoped<IHostelStudentAppService, HostelStudentAppService> ();

// --------------------
// MVC + Swagger
// --------------------
builder.Services.AddControllers ();
builder.Services.AddMemoryCache ();
builder.Services.AddEndpointsApiExplorer ();

builder.Services.AddSwaggerGen ( c =>
{
    c.SwaggerDoc ( "v1", new OpenApiInfo { Title = "Hostel Service API", Version = "v1" } );
} );

var app = builder.Build ();

// --------------------
// Middleware Pipeline
// --------------------
if (app.Environment.IsDevelopment ())
{
    app.UseSwagger ();
    app.UseSwaggerUI ();
}

app.UseHttpsRedirection ();
app.MapControllers ();
app.Run ();
