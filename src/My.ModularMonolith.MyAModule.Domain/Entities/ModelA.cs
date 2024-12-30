using My.ModularMonolith.Common.Domain.Entities;

namespace My.ModularMonolith.MyAModule.Domain.Entities;

public class ModelA : BaseEntity<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }

    public static ModelA Create(string name, string description)
    {
        return new ModelA
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description
        };
    }
    
    // internal ModelA()
    // {
    // }
}