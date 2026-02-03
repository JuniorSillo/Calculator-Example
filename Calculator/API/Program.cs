using Microsoft.OpenApi.Models;                    // Often required for full IntelliSense
using Swashbuckle.AspNetCore.Swagger;              // ← Add this (for UseSwagger / UseSwaggerUI)
using Swashbuckle.AspNetCore.SwaggerGen;           // ← Add this (for AddSwaggerGen)

using CalculatorDomain.Logic;
using CalculatorDomain.Persistence;
using CalculatorDomainDemo.Persistence;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// ── These two are very important in .NET 6+ ──
builder.Services.AddEndpointsApiExplorer();        // Required for good metadata (even with controllers)
builder.Services.AddSwaggerGen();                  // Should now be recognized

builder.Services.AddSingleton<ICalculationStore>(new FileCalculationStore("calculations.json"));
builder.Services.AddSingleton<CalculatorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();                              // Should now resolve
    app.UseSwaggerUI();                            // Should now resolve
    // Optional: customize UI
    // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Calculator API v1"));
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();