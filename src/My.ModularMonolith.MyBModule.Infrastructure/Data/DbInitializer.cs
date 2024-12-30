using System.Diagnostics.CodeAnalysis;

namespace My.ModularMonolith.MyBModule.Infrastructure.Data;

[ExcludeFromCodeCoverage]
public static class DbInitializer
{
    public static async Task Run(MyBModuleContext context)
    {
        await context.Database.EnsureCreatedAsync();
    }
}