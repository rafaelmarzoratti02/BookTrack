using BookTrack.Application.Commands.BookCommands.AddBook;
using BookTrack.Core.Entitites;
using BookTrack.Shared.InputModels;
using BookTrack.Shared.InputModels.Books;
using FluentValidation;

namespace BookTrack.Application.Validators;

public class CreateBookInputModelValidator  : AbstractValidator<InsertBookCommand>
{
    public CreateBookInputModelValidator ()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required");
        RuleFor(x => x.Title)
            .MaximumLength(50).WithMessage("Title cannot exceed 50 characters");
        RuleFor(x => x.ISBN)
            .NotEmpty().WithMessage("ISBN is required");
        RuleFor(x => x.ISBN)
            .Length(13).WithMessage("ISBN must have 13 characters");
    }
}