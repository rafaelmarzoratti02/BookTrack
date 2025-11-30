using BookTrack.Shared.InputModels.Reviews;
using FluentValidation;

namespace BookTrack.Application.Validators;

public class CreateReviewInputModelValidator : AbstractValidator<CreateReviewInputModel>
{
    public CreateReviewInputModelValidator()
    {
        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5)
            .WithMessage("Rating must be between 1 and 5");
        
        RuleFor(x => x.ReadingStartDate)
            .LessThanOrEqualTo(x=> x.ReadingEndDate)
            .WithMessage("Reading end date must be greater than or equal to date");
    }
}