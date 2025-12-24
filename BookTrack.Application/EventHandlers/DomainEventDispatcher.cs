using BookTrack.Core.Events;

namespace BookTrack.Application.EventHandlers;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var eventType = domainEvent.GetType();
        
        var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(eventType);
        
        var handlers = _serviceProvider.GetService(handlerType);

        if (handlers != null)
        {
            var handleMethod = handlerType.GetMethod("Handle");
            if (handleMethod != null)
            {
                var task = (Task)handleMethod.Invoke(handlers, new object[] { domainEvent });
                if (task != null)
                {
                    await task;
                }
            }
        }
    }

    public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in domainEvents)
        {
            await DispatchAsync(domainEvent, cancellationToken);
        }
    }
}
