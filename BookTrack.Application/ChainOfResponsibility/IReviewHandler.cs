using BookTrack.Application.Commands.ReviewsCommands.InsertReview;
using BookTrack.Shared.InputModels.Reviews;

namespace BookTrack.Application.ChainOfResponsibility;

public interface IReviewHandler
{
    Task Handle(InsertReviewCommand model);
    IReviewHandler SetNext(IReviewHandler handler);
}