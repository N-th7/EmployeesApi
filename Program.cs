using DotNetEnv;
using AvancApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
    $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
    $"Username={Environment.GetEnvironmentVariable("DB_USER")};" +
    $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}";

// 1) CORS: permite al frontend (Vite) llamar al backend
const string FrontendOrigin = "http://localhost:5173";
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins(FrontendOrigin)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // déjalo si usarás cookies/autenticación; si no, puedes quitarlo
    });
});

// 2) API versioning (si lo usas en controllers)
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

builder.Services.AddDbContext<EmployeeContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 3) Swagger solo en Development (opcional pero recomendado)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 4) HTTPS redirection (si usarás https en dev con cert confiable)
// app.UseHttpsRedirection();

// 5) CORS DEBE ir antes de MapControllers
app.UseCors("FrontendPolicy");

app.UseAuthorization();
app.MapControllers();

app.Run();
