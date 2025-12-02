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
var applyMigrationsEnv = builder.Configuration["APPLY_MIGRATIONS"];
var applyMigrations = false;

// If env var is explicitly set to "true", honor it.
// Otherwise allow auto-run only in Development.
if (!string.IsNullOrEmpty ( applyMigrationsEnv ))
{
    bool.TryParse ( applyMigrationsEnv, out applyMigrations );
}
else
{
    applyMigrations = app.Environment.IsDevelopment ();
}

if (applyMigrations)
{
    using var scope = app.Services.CreateScope ();
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>> ();
    var db = services.GetRequiredService<HostelDbContext> ();

    const int maxRetries = 6;
    int attempt = 0;
    while (true)
    {
        try
        {
            attempt++;
            logger.LogInformation ( "Attempting database migration (attempt {Attempt}/{Max})...", attempt, maxRetries );
            // This will create DB if missing and apply only pending migrations.
            db.Database.Migrate ();
            logger.LogInformation ( "Database migrations applied successfully." );
            break;
        }
        catch (Exception ex)
        {
            logger.LogWarning ( ex, "Database migration attempt {Attempt} failed.", attempt );
            if (attempt >= maxRetries)
            {
                // If we can't migrate after retries, rethrow to fail fast, or optionally continue without DB depending on your policy.
                logger.LogError ( ex, "Failed to apply migrations after {Attempts} attempts. Aborting startup.", attempt );
                throw;
            }

            // Exponential backoff (2^attempt seconds)
            var delaySeconds = Math.Min ( 30, (int)Math.Pow ( 2, attempt ) );
            logger.LogInformation ( "Waiting {Delay}s before next migration attempt...", delaySeconds );
            Thread.Sleep ( TimeSpan.FromSeconds ( delaySeconds ) );
        }
    }
}
else
{
    // Log info so you know migrations were intentionally skipped.
    var logger = app.Services.GetRequiredService<ILogger<Program>> ();
    logger.LogInformation ( "APPLY_MIGRATIONS is false and environment is not Development. Skipping automatic migrations." );
}

app.Run ();
