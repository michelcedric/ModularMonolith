using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace My.ModularMonolith.Api.Filters;

/// <summary>
/// Necessary to correctly manage nullability property on from form data
/// </summary>
[ExcludeFromCodeCoverage]
internal class NullabilitySchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        // if the MemberInfo is a PropertyInfo then it's a property
        if (context.MemberInfo is PropertyInfo propertyInfo)
        {
            // create a new NullabilityInfoContext that we can use to extract Nullability info about the PropertyInfo
            // the overhead of multiple creations of this seem negligible 
            var nullabilityInfoContext = new NullabilityInfoContext();

            // get the NullabilityInfo for the PropertyInfo
            var nullabilityInfo = nullabilityInfoContext.Create(propertyInfo);

            // check the nullability state of the Write/Setter 
            if (nullabilityInfo.WriteState is NullabilityState.Nullable)
            {
                // update the schema with Nullable information
                schema.Nullable = true;
            }
        }
    }
}