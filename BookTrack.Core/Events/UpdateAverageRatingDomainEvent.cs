using BookTrack.Core.Entitites;

namespace BookTrack.Core.Events;

public class UpdateAverageRatingDomainEvent : IDomainEvent
{
    public UpdateAverageRatingDomainEvent(int bookId)
    {
        BookId = bookId;
    }

    public int BookId { get; private set; }
}