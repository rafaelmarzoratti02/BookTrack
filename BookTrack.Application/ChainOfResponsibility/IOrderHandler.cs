using BookTrack.Shared.InputModels.Reviews;

namespace BookTrack.Application.ChainOfResponsibility;

public interface IOrderHandler
{
    Task Handle(CreateReviewInputModel model);
    IOrderHandler SetNext(IOrderHandler handler);
}