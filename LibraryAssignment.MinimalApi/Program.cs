using FluentValidation;
using LibraryAssignment.Data.DbContext;
using LibraryAssignment.Data.DTOs;
using LibraryAssignment.Data.Models;
using LibraryAssignment.MinimalApi.Interfaces;
using LibraryAssignment.MinimalApi.Repositories;
using LibraryAssignment.MinimalApi.Validations;
using Microsoft.AspNetCore.Mvc;
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
builder.Services.AddValidatorsFromAssemblyContaining<CreateBookDto>();
builder.Services.AddTransient<IValidator<CreateBookDto>, CreateBookValidations>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
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
            return books.Any() ? Results.Ok(books) : Results.NotFound();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error when getting books");
            return Results.StatusCode(500);
        }})
    .Produces(200)
    .Produces<List<Book>>()
    .Produces(500)
    .Produces(404);

app.MapGet("/api/books/{id:int}", async (int id, IRepository<Book> repository, ILogger logger) =>
    {
        try
        {
            var book = await repository.Get(id);
            return book != null ? Results.Ok(book) : Results.NotFound();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error when getting book");
            return Results.StatusCode(500);
        }})
    .Produces(200)
    .Produces<Book>()
    .Produces(500)
    .Produces(404);

app.MapPost("api/books", async ([FromBody] CreateBookDto bookDto, IRepository<Book> repository, 
        ILogger logger, IValidator<CreateBookDto> validator) =>
    {
        try
        {
            var validationResult = await validator.ValidateAsync(bookDto);
            
             if (!validationResult.IsValid)
             {
                 return Results.BadRequest(validationResult.Errors);
             }
            
            var book = new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                Description = bookDto.Description,
                IsAvailable = bookDto.IsAvailable
            };

            var createdBook = await repository.Create(book);
            
            return createdBook != null ? Results.Created($"/api/books/{createdBook.Id}", createdBook) : Results.StatusCode(500);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error when creating book");
            return Results.StatusCode(500);
        }})
    .Produces(201)
    .Produces<Book>()
    .Produces(500);

app.Run();