namespace My.ModularMonolith.Common.Domain.Entities;

/// <summary>
/// Base entity definition
/// </summary>
public abstract class BaseEntity<TKey>(TKey id)
    where TKey : notnull
{
    /// <summary>
    /// The unique identifier of an entity
    /// </summary>
    public TKey Id { get; init; } = id;
}