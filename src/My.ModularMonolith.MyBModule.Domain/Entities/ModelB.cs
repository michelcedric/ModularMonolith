using My.ModularMonolith.Common.Domain.Entities;

namespace My.ModularMonolith.MyBModule.Domain.Entities;

public class ModelB :  BaseEntity<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }

    public static ModelB Create(string name, string description)
    {
        return new ModelB
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description
        };
    }
    
    internal ModelB()
    {
    }
}