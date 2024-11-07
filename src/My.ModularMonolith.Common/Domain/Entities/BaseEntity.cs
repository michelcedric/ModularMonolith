namespace My.ModularMonolith.Common.Domain.Entities;

/// <summary>
/// Base entity definition
/// </summary>
public abstract class BaseEntity<TKey> where TKey : notnull
{
    /// <summary>
    /// The unique identifier of an entity
    /// </summary>
    public virtual TKey Id { get; set; }
}