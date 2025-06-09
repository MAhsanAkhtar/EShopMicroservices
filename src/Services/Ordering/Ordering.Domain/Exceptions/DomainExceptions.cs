namespace Ordering.Domain.Exceptions;
using System;
public class DomainEventException : Exception
{
    public DomainEventException(string message)
        : base($"Domain Exception: \"{message}\" throws from Domain Layer.")
    {
    }
}
