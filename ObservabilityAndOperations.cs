// Ova datoteka simulira postavku ASP.NET Core aplikacije.
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// GREŠKA: Nedovoljne prijave (logging). Ovo koristi jednostavan Console.WriteLine umjesto strukturiranog loggera.
// To otežava pretraživanje, filtriranje i analizu logova u produkcionom okruženju.
app.Use((context, next) =>
{
    Console.WriteLine($"Request received for path: {context.Request.Path}");
    return next();
});

// GREŠKA: Nedostaje endpoint za health check.
// Ne postoji '/health' endpoint koji bi sistemi za nadzor (npr. Kubernetes ili load balancer)
// koristili da provjere radi li aplikacija ispravno.
// Ispravna konfiguracija bi uključivala:
// builder.Services.AddHealthChecks();
// app.MapHealthChecks("/health");

app.MapGet("/", () => "Hello World!");

// GREŠKA: Iscrpljivanje resursa / ranjivost na Denial-of-Service (DoS).
// Ovaj endpoint prima upload fajla iz forme bez ograničenja veličine.
// Napadač bi mogao poslati ekstremno veliki fajl i potrošiti svu dostupnu memoriju ili prostor na disku.
app.MapPost("/upload", async (IFormFile file) =>
{
    // Nema provjere veličine za 'file'.
    using var stream = System.IO.File.Create($"uploads/{file.FileName}");
    await file.CopyToAsync(stream);
    return Results.Ok("File uploaded.");
});

app.Run();
