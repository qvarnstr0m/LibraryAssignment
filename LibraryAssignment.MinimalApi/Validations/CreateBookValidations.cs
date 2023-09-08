using FluentValidation;
using LibraryAssignment.MinimalApi.DTOs;

namespace LibraryAssignment.MinimalApi.Validations;

public class CreateBookValidations : AbstractValidator<CreateBookDto>
{
    public CreateBookValidations()
    {
        RuleFor(book => book.Title)
            .NotEmpty()
            .WithMessage("Book title is required")
            .MaximumLength(50)
            .WithMessage("Book title cannot be longer than 50 characters");

        RuleFor(book => book.Author)
            .NotEmpty()
            .WithMessage("Book author is required")
            .MaximumLength(100)
            .WithMessage("Book author cannot be longer than 100 characters");
        RuleFor(book => book.Isbn)
            .NotEmpty()
            .WithMessage("Book Isbn is required")
            .MaximumLength(50)
            .WithMessage("Book Isbn cannot be longer than 50 characters");

    }
}