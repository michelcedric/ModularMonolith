using Microsoft.FeatureManagement;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace My.ModularMonolith.Api.Filters;

public class FeatureGateDocumentFilter : IDocumentFilter
{
    private readonly IFeatureManager _featureManager;
    
    public FeatureGateDocumentFilter(IFeatureManager featureManager)
    {
        _featureManager = featureManager;
    }

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var apiDescription in context.ApiDescriptions)
        {
            var currentFeature = apiDescription.RelativePath?.Split('/').First();
            var isActive = _featureManager.IsEnabledAsync($"{ModuleDefinition.ModulePrefix}{currentFeature}").GetAwaiter().GetResult();
            if (!isActive && !string.IsNullOrEmpty(apiDescription.RelativePath))
            {
                var apiPath = swaggerDoc.Paths.FirstOrDefault(o => o.Key.Contains(apiDescription.RelativePath));

                if (!apiPath.Equals(default(KeyValuePair<string, OpenApiPathItem>)))
                {
                    swaggerDoc.Paths.Remove(apiPath.Key);
                }
            }
        }
    }
}