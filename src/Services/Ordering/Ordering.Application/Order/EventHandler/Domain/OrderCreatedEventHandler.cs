﻿using MassTransit;
using Microsoft.FeatureManagement;

namespace Ordering.Application.Order.EventHandler.Domain;

public class OrderCreatedEventHandler
    (IPublishEndpoint publishEndpoint, IFeatureManager featureManager, ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        if(await featureManager.IsEnabledAsync("OrderFullfilment"))
        {
            var orderCreateIntegrationEvent = domainEvent.order.ToOrderDto();
            await publishEndpoint.Publish(orderCreateIntegrationEvent, cancellationToken);
        }
  
    }


}
