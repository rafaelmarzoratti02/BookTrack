using BookTrack.Core.Entitites;
using FluentValidation;

namespace BookTrack.Application.Validators;

public class InsertBookValidator : AbstractValidator<Book>
{
    public InsertBookValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(50).WithMessage("Title cannot exceed 50 characters");
        RuleFor(x => x.ISBN)
            .Length(13).WithMessage("ISBN cannot exceed 13 characters");
    }
}