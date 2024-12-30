using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using My.ModularMonolith.MyAModule.Domain.Entities;

namespace My.ModularMonolith.MyAModule.Infrastructure.Data;

[ExcludeFromCodeCoverage]
public static class DbInitializer
{
    public static async Task Run(MyAModuleContext context)
    {
        await context.Database.EnsureCreatedAsync();
        if (!context.ModelAs.Any())
        {
            context.ModelAs.AddRange(AModels);
            await context.SaveChangesAsync();
        }
    }
    
    private static readonly ModelA[] _aModels =
    [
        ModelA.Create("AA","Description model A 1"),
        ModelA.Create("BB","Description model A 2"),
        ModelA.Create("CC","Description model A 3"),
    ];
    
    public static readonly ReadOnlyCollection<ModelA> AModels = new(_aModels);
}