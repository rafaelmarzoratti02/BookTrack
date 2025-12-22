using BookTrack.Shared.InputModels.Reviews;

namespace BookTrack.Application.ChainOfResponsibility;

public abstract class OrderHandler : IOrderHandler
{
    private IOrderHandler? _nextHandler;
    public virtual async Task Handle(CreateReviewInputModel model)
    {
        if (_nextHandler != null)
            await _nextHandler.Handle(model);
    }

    public IOrderHandler SetNext(IOrderHandler step)
    {
        _nextHandler = step;

        return step;
    }
}