using Microsoft.R9.Extensions.Tracing.Http;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddRedaction();

builder.Services
    .AddOpenTelemetry().WithTracing(builder =>
    {
        builder
            .AddSource("SampleActivitySourceTwo")
            .SetSampler(new TraceIdRatioBasedSampler(1.0))
            .AddHttpClientInstrumentation()
            .AddHttpTracing()
            .AddAspNetCoreInstrumentation()
            .AddConsoleExporter();
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
