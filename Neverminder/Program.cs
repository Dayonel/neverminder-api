using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Neverminder.Data;
using Neverminder.DI;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1.0.4",
        Title = "Neverminder API",
        Description = "Reminders app that sends notifications for your plans.",
    });
});
builder.Services.AddRouting(o => o.LowercaseUrls = true);

// DI
builder.Services.AddDependencies(builder.Configuration);

// Add logging
if (!builder.Environment.IsDevelopment())
{
    var log = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.File("logs/neverminderapi-.txt", rollingInterval: RollingInterval.Day)
        .MinimumLevel.Warning()
        .CreateLogger();

    builder.Host.UseSerilog(log);
}

var app = builder.Build();

// DB migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<NeverminderDbContext>();
    db.Database.Migrate();
    db.Database.ExecuteSqlRaw("PRAGMA wal_checkpoint;"); // performs a checkpoint operation transferring the committed changes
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowAnyOrigin();
});

app.UseAuthorization();

app.MapControllers();

app.Run();