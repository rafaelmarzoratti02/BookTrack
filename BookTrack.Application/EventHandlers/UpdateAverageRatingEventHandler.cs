using BookTrack.Core.Events;
using BookTrack.Core.Repositories;

namespace BookTrack.Application.EventHandlers;

public class UpdateAverageRatingEventHandler : IDomainEventHandler<UpdateAverageRatingDomainEvent>
{
    private readonly IBookRepository _bookRepository;

    public UpdateAverageRatingEventHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task Handle(UpdateAverageRatingDomainEvent domainEvent)
    {
        var book = await _bookRepository.GetById(domainEvent.BookId);

        if (book != null && book.Reviews.Any())
        {
            book.AverageRating = (decimal)book.Reviews.Average(r => r.Rating);
        }
    }
}

