using Microsoft.FeatureManagement;

namespace My.ModularMonolith.Api;

public class ModuleDefinition
{
    public const string ModulePrefix = "Module";
    public string Name { get; }
    public int Number { get; }
    public string Title => $"{Number} - {FullName}";
    public bool IsEnabled { get; private set; }
    private string Id { get; }
    private string FullName { get; }

    private ModuleDefinition(string id, string name, string fullName, int number)
    {
        Id = id;
        Name = name;
        FullName = fullName;
        Number = number;
        IsEnabled = false;
    }

    private static readonly ModuleDefinition[] ModuleDefinitions =
    [
        new("ModuleMyAModule", "MyAModule", "The module A", 1),
        new("ModuleMyBModule", "MyBModule", "The module B", 2),
    ];

    public static ModuleDefinition GetModuleDefinition(string id, IFeatureManager featureManager)
    {
        var moduleDefinition = ModuleDefinitions.First(x => x.Id == id);
        moduleDefinition.IsEnabled = featureManager.IsEnabledAsync(id).GetAwaiter().GetResult();
        return moduleDefinition;
    }
}