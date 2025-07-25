﻿namespace Ordering.Domain.Models;

public class Order: Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = new();

    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public CustomerId CustomerId { get; private set; } = default!;

    public OrderName OrderName { get; private set; } = default!;

    public Address ShippingAddress { get; private set; } = default!;

    public Address BillingAddress { get; private set; } = default!;

    public Payment Payment { get; private set; } = default!;


    public OrderStatus Status { get; private set; } = OrderStatus.Pending;

    public decimal TotalPrice
    {
        get => OrderItems.Sum(x => x.Price * x.Quantity);
        private set { }
    }

    // Static Create Method
    public static Order Create(OrderId id, CustomerId customerId,OrderName orderName,Address shippingAddress,Address billingAddress, Payment payment)
    {
        //default constructor calling
        var order = new Order()
        {
            Id = id,
            CustomerId = customerId,
            OrderName = orderName,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            Payment = payment,
            Status = OrderStatus.Pending

        };
        order.AddDomainEvent(new OrderCreatedEvent(order));
        return order; 
    }

    // Update method
    public void Update(OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment,OrderStatus status)
    {
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        Status = status;

        AddDomainEvent(new OrderUpdatedEvent(this));

    }

    // Add Method
    public void Add(ProductId productId, int quantity, decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var orderItem = new OrderItem(Id, productId, quantity, price);
        _orderItems.Add(orderItem);
    }

    // Remove method
    public void Remove(ProductId productId)
    {
        var orderItem = _orderItems.FirstOrDefault(x => x.ProductId == productId);
        if (orderItem != null)
        {
            _orderItems.Remove(orderItem);
        }

    }
}
