using BookTrack.Shared.InputModels.Reviews;

namespace BookTrack.Application.ChainOfResponsibility;

public interface IReviewHandler
{
    Task Handle(CreateReviewInputModel model);
    IReviewHandler SetNext(IReviewHandler handler);
}