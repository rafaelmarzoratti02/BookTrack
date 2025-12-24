using BookTrack.Core.Events;

namespace BookTrack.Core.Entitites;

public class BaseEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public BaseEntity()
    {
        IsActive = true;
        CreatedOn = DateTime.Now;
    }

    public int Id { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedOn { get; set; }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void SetAsDeleted()
    {
        IsActive = false;
    }
}   