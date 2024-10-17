using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.FeatureManagement;

namespace My.ModularMonolith.Api.Extensions;

public static class Extensions
{
    public static ModuleDefinition[] GetEnabledModules(this IFeatureManager featureManager)
    {
        var modules = featureManager.GetFeatureNamesAsync().ToBlockingEnumerable().Where(f => f.StartsWith(ModuleDefinition.ModulePrefix))
            .Select(f => ModuleDefinition.GetModuleDefinition(f, featureManager)).Where(m => m.IsEnabled).OrderBy(m => m.Number).ToArray();
        return modules;
    }
    
    public static string ApplyPascalCase(this string input)
    {
        return string.IsNullOrEmpty(input) ? string.Empty : $"{input[0].ToString().ToUpper()}{input[1..].ToLower()}";
    }

    public static string UpperFirstChar(this string input)
    {
        return string.IsNullOrEmpty(input) ? string.Empty : $"{input[0].ToString().ToUpper()}{input[1..]}";
    }

    public static void CleanModulesPart(this ApplicationPartManager applicationPartManager, WebApplicationBuilder builder,string baseNamespace)
    {
        var featureManager = builder.Services.BuildServiceProvider().GetRequiredService<IFeatureManager>();
        var modules = featureManager.GetEnabledModules();
        var partsToRemove = applicationPartManager.ApplicationParts.Count(b => b.Name.Contains(baseNamespace, StringComparison.InvariantCultureIgnoreCase));
        for (var i = 0; i < partsToRemove; i++)
        {
            applicationPartManager.ApplicationParts.Remove(applicationPartManager.ApplicationParts.First(b => b.Name.Contains(baseNamespace, StringComparison.InvariantCultureIgnoreCase)));
        }
        foreach (var module in modules)
        {
            applicationPartManager.ApplicationParts.Add(
                new AssemblyPart(AppDomain.CurrentDomain.GetAssemblies()
                    .First(g => g.FullName!.Contains($"{baseNamespace}{module.Name}.Api,", StringComparison.InvariantCultureIgnoreCase))));
        }
    }
}