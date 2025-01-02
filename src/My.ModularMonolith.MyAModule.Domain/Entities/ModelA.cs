using My.ModularMonolith.Common.Domain.Entities;

namespace My.ModularMonolith.MyAModule.Domain.Entities;

public sealed class ModelA : BaseEntity<Guid>
{
    public string Name { get; set; }
    public string? Description { get; set; }

    public static ModelA Create(string name, string? description)
    {
        return new ModelA(name, description);
    }

    public void Update(string name, string? description)
    {
         Name = name;
         Description = description;
    }

    private ModelA(string name, string? description) : base(Guid.NewGuid())
    {
        Name = name;
        Description = description;
    }
}