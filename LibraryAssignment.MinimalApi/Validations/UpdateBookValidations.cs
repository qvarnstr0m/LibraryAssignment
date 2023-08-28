using FluentValidation;
using LibraryAssignment.Data.DTOs;

namespace LibraryAssignment.MinimalApi.Validations;

public class UpdateBookValidations : AbstractValidator<UpdateBookDto>
{
    public UpdateBookValidations()
    {
        RuleFor(book => book.Id)
            .NotEmpty()
            .WithMessage("Book id is required")
            .GreaterThan(0)
            .WithMessage("Book id must be greater than 0");
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
    }
}