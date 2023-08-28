using LibraryAssignment.Data.DbContext;
using LibraryAssignment.Data.Models;
using LibraryAssignment.MinimalApi.Interfaces;
using LibraryAssignment.MinimalApi.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

// Add services to the container.
builder.Services.AddScoped<IRepository<Book>, Repository<Book>>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(configuration.GetConnectionString("LocalConnection"), new MySqlServerVersion(new Version(8, 0, 32)));
});
builder.Services.AddScoped<ILogger, Logger<Program>>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/books", async (IRepository<Book> repository, ILogger logger) =>
    {
        try
        {
            var books = await repository.GetAll();
            return Results.Ok(books);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error when getting books");
            return Results.StatusCode(500);
        }})
    .Produces(200)
    .Produces<List<Book>>()
    .Produces(500);

app.Run();