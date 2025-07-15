namespace Ordering.Application.Order.EventHandler.Domain;

public class OrderUpdateEventHandler(ILogger<OrderUpdatedEvent> logger) :
    INotificationHandler<OrderUpdatedEvent>
{
    public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
