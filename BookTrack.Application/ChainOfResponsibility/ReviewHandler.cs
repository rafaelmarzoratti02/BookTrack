using BookTrack.Shared.InputModels.Reviews;

namespace BookTrack.Application.ChainOfResponsibility;

public abstract class ReviewHandler : IReviewHandler
{
    private IReviewHandler? _nextHandler;
    public virtual async Task Handle(CreateReviewInputModel model)
    {
        if (_nextHandler != null)
            await _nextHandler.Handle(model);
    }

    public IReviewHandler SetNext(IReviewHandler step)
    {
        _nextHandler = step;

        return step;
    }
}