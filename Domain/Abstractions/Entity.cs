namespace Domain.Abstractions;

public class Entity<T> : IEntity<T>
{
    public required T Id { get; set; }
}