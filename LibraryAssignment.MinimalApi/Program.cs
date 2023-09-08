using AutoMapper;
using FluentValidation;
using LibraryAssignment.Data.DbContext;
using LibraryAssignment.MinimalApi.DTOs;
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
        builder.WithOrigins("https://localhost:7067", "http://localhost:5173")
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
        }
    })
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
        }
    })
    .Produces(200)
    .Produces<Book>()
    .Produces(500)
    .Produces(404);

app.MapPost("api/books", async ([FromBody] CreateBookDto bookDto, IRepository<Book> repository,
        ILogger logger, IValidator<CreateBookDto> validator, IMapper mapper) =>
    {
        try
        {
            var validationResult = await validator.ValidateAsync(bookDto);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }

            var createdBook = await repository.Create(mapper.Map<Book>(bookDto));

            return createdBook != null
                ? Results.Created($"/api/books/{createdBook.Id}", createdBook)
                : Results.StatusCode(500);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error when creating book");
            return Results.StatusCode(500);
        }
    })
    .Produces(201)
    .Produces<Book>()
    .Produces(500)
    .Produces(400);

app.MapPut("api/books/{id:int}", async (int id, [FromBody] UpdateBookDto bookDto,
        IRepository<Book> repository, ILogger logger, IValidator<UpdateBookDto> validator, IMapper mapper) =>
    {
        try
        {
            if (id != bookDto.Id)
            {
                return Results.BadRequest("Book id does not match");
            }

            var validationResult = await validator.ValidateAsync(bookDto);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }

            var existingBook = await repository.Get(id);

            if (existingBook == null)
            {
                return Results.NotFound();
            }

            mapper.Map(bookDto, existingBook);

            var updatedBook = await repository.Update(bookDto.Id);

            return updatedBook != null ? Results.StatusCode(204) : Results.StatusCode(500);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error when updating book");
            return Results.StatusCode(500);
        }
    })
    .Produces(204)
    .Produces(500)
    .Produces(400)
    .Produces(404);

app.MapDelete("api/books/{id:int}", async (int id, IRepository<Book> repository, ILogger logger) =>
    {
        try
        {
            var existingBook = await repository.Get(id);

            if (existingBook == null)
            {
                return Results.NotFound();
            }

            var deletedBook = await repository.Delete(id);

            return deletedBook != null ? Results.StatusCode(204) : Results.StatusCode(500);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error when deleting book");
            return Results.StatusCode(500);
        }
    })
    .Produces(204)
    .Produces(500)
    .Produces(404);

app.MapGet("/api/books/search", async (IBookRepository bookRepository, ILogger logger,
        [FromQuery] string? searchWord) =>
    {
        try
        {
            if (string.IsNullOrEmpty(searchWord))
            {
                return Results.BadRequest("Search word cannot be empty");
            }

            var books = await bookRepository.SearchBooks(searchWord);

            return Results.Ok(books);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error when searching for books");
            return Results.StatusCode(500);
        }
    })
    .Produces(200)
    .Produces<List<Book>>()
    .Produces(500)
    .Produces(400);

app.Run();