// This file simulates an ASP.NET Core application setup.
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// ERROR: Insufficient logging. This uses simple Console.WriteLine instead of a structured logger.
// This makes logs hard to search, filter, and analyze in a production environment.
app.Use((context, next) =>
{
    Console.WriteLine($"Request received for path: {context.Request.Path}");
    return next();
});

// ERROR: Missing health check endpoint.
// There is no '/health' endpoint for monitoring systems (like Kubernetes or a load balancer)
// to check if the application is running correctly.
// A proper setup would include:
// builder.Services.AddHealthChecks();
// app.MapHealthChecks("/health");

app.MapGet("/", () => "Hello World!");

// ERROR: Resource exhaustion / Denial-of-Service (DoS) vulnerability.
// This endpoint accepts a form file upload without any size limits.
// An attacker could upload an extremely large file, consuming all available memory or disk space.
app.MapPost("/upload", async (IFormFile file) =>
{
    // No size validation is performed on 'file'.
    using var stream = System.IO.File.Create($"uploads/{file.FileName}");
    await file.CopyToAsync(stream);
    return Results.Ok("File uploaded.");
});

app.Run();