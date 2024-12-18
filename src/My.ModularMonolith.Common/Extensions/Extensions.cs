using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace My.ModularMonolith.Common.Extensions;

public static class Extensions
{
    public static bool AppUseOnlyInMemoryDatabase(this IHostApplicationBuilder builder)
    {
        var useOnlyInMemoryDatabase = false;
        if (builder.Configuration["UseOnlyInMemoryDatabase"] != null)
        {
            useOnlyInMemoryDatabase = bool.Parse(builder.Configuration["UseOnlyInMemoryDatabase"] ?? "");
        }
        return useOnlyInMemoryDatabase;
    }
    public static void AddRepositories(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();
        var repositoryTypes = assembly.GetTypes()
            .Where(t => t.Name.EndsWith("Repository") && t is { IsClass: true, IsAbstract: false });

        foreach (var implementationType in repositoryTypes)
        {
            var interfaceType = implementationType.GetInterfaces()
                .SingleOrDefault(i => i.Name == $"I{implementationType.Name}");

            if (interfaceType != null)
            {
                services.AddScoped(interfaceType, implementationType);
            }
            else
            {
                throw new InvalidOperationException($"No interface found for {implementationType.Name}");
            }
        }
    }
}