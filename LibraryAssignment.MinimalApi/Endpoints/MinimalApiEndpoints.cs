using AutoMapper;
using FluentValidation;
using LibraryAssignment.Data.Models;
using LibraryAssignment.MinimalApi.DTOs;
using LibraryAssignment.MinimalApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAssignment.MinimalApi.Endpoints;

public static class MinimalApiEndpoints
{
    public static void RegisterEndpoints(WebApplication app)
    {
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
    }
}