using FluentValidation;
using LibraryAssignment.Data.DbContext;
using LibraryAssignment.MinimalApi.DTOs;
using LibraryAssignment.Data.Models;
using LibraryAssignment.MinimalApi.Endpoints;
using LibraryAssignment.MinimalApi.Interfaces;
using LibraryAssignment.MinimalApi.Repositories;
using LibraryAssignment.MinimalApi.Validations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

// Add services to the container.
builder.Services.AddScoped<IRepository<Book>, Repository<Book>>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(configuration.GetConnectionString("LocalConnection"),
        new MySqlServerVersion(new Version(8, 0, 32)));
});
builder.Services.AddValidatorsFromAssemblyContaining<CreateBookDto>();
builder.Services.AddTransient<IValidator<CreateBookDto>, CreateBookValidations>();
builder.Services.AddTransient<IValidator<UpdateBookDto>, UpdateBookValidations>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ILogger, Logger<Program>>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("https://localhost:7067", "http://localhost:5173", "http://127.0.0.1:5173")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

MinimalApiEndpoints.RegisterEndpoints(app);

app.Run();