using AspireApp.ApiService.Database;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using FluentValidation;
using AspireApp.ApiService.Endpoints;
using AspireApp.ApiService.Database.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("postgresdb")));

builder.Services.AddEndpoints();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

    app.ApplyMigrations();
}
app.MapEndpoints();
app.MapDefaultEndpoints();



app.Run();
