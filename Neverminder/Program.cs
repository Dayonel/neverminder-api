using Microsoft.OpenApi.Models;
using Neverminder.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1.0.2",
        Title = "Neverminder API",
        Description = "Reminders app that sends notifications for your plans.",
    });
});

// DI
builder.Services.AddDependencies(builder.Configuration);

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();