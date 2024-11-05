using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.OpenApi.Models;
using My.ModularMonolith.Api.Filters.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace My.ModularMonolith.Api.Filters;

[ExcludeFromCodeCoverage]
internal class GeneralExceptionOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Responses.Add(StatusCodes.Status500InternalServerError.ToString(), new OpenApiResponse()
        {
            Description = "Internal server error",
            Content = new Dictionary<string, OpenApiMediaType>()
            {
                ["application/json"] = new()
                {
                    Schema = context.SchemaGenerator.GenerateSchema(typeof(GenericError), context.SchemaRepository)
                }
            }
        });

        var method = context.MethodInfo.GetCustomAttributes(true)
            .OfType<HttpMethodAttribute>()
            .SingleOrDefault();

        if (method is HttpPostAttribute or HttpPutAttribute or HttpDeleteAttribute)
        {
            operation.Responses.Add(StatusCodes.Status400BadRequest.ToString(), new OpenApiResponse()
            {
                Description = "BadRequest",
                Content = new Dictionary<string, OpenApiMediaType>()
                {
                    ["application/json"] = new()
                    {
                        Schema = context.SchemaGenerator.GenerateSchema(typeof(ValidationProblemDetails), context.SchemaRepository)
                    }
                }
            });
        }
    }
}