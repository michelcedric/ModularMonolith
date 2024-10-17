using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.FeatureManagement;
using Microsoft.OpenApi.Models;
using My.ModularMonolith.Api.Filters;

namespace My.ModularMonolith.Api.Extensions;

public static class SwaggerExtensions
{
    public static void UseModuleSwagger(this WebApplication app, string environmentName)
    {
        var featureManager = app.Services.GetRequiredService<IFeatureManager>();
        var modules = featureManager.GetEnabledModules();
        if (environmentName is "Local" or "integrationTest")
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                options.SwaggerEndpoint("v1/swagger.json", "Full");

                foreach (var module in modules)
                {
                    options.SwaggerEndpoint($"{module.Name}/swagger.json", module.Title);
                }
            });
        }
    }
    public static void AddModuleSwaggerGen(this WebApplicationBuilder builder)
    {
        if (builder.Environment.EnvironmentName is "Local" or "integrationTest")
        {
            var featureManager = builder.Services.BuildServiceProvider().GetRequiredService<IFeatureManager>();
            var modules = featureManager.GetEnabledModules();

            builder.Services.AddSwaggerGen((options) =>
            {
                options.UseAllOfToExtendReferenceSchemas();
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Documentation for full API usage",
                    Version = "1.0",
                    Description = "Documentation for api"
                });
                
                foreach (var module in modules)
                {
                    options.SwaggerDoc(module.Name, new OpenApiInfo
                    {
                        Title = $"Documentation for {module.Name}",
                        Version = "1.0",
                        Description = $"Documentation for {module.Name} API usage"
                    });
                }

                options.DocInclusionPredicate((d, apiDesc) =>
                {
                    if (string.Compare(d, "v1", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        return true;
                    }

                    if (apiDesc.RelativePath != null)
                    {
                        return apiDesc.RelativePath.StartsWith(d, StringComparison.InvariantCultureIgnoreCase);
                    }

                    return true;
                });

                options.CustomOperationIds(e =>
                {
                    ArgumentNullException.ThrowIfNull(e.RelativePath);
                    var cleanRelativePath = e.RelativePath.Replace("/", "").Replace("$", "").Replace("{", "")
                        .Replace("}", "").Replace("(", "").Replace(")", "");
                    var httpMethod = e.HttpMethod?.ApplyPascalCase();
                    var operation = cleanRelativePath.Remove(0, e.RelativePath.IndexOf("/", StringComparison.InvariantCultureIgnoreCase)).UpperFirstChar();
                    var operationId = $"{httpMethod}{operation}";
                    return operationId;
                });

                options.TagActionsBy(api =>
                {
                    var module = modules.FirstOrDefault(m => api.ActionDescriptor.DisplayName != null && api.ActionDescriptor.DisplayName.Contains(m.Name));
                    if (module != null)
                    {
                        if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                        {
                            return [$"{module.Title} - {controllerActionDescriptor.ControllerName}"];
                        }
                    }
                    return ["Other"];
                });

                options.DocumentFilter<FeatureGateDocumentFilter>();
            });
        }

        builder.Services.AddEndpointsApiExplorer();
    }
}