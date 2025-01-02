using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using My.ModularMonolith.Common.Domain.Entities;
using My.ModularMonolith.MyBModule.Domain.Entities;

namespace My.ModularMonolith.MyBModule.Api.Controllers.OData;

public static class Builder
{
    public static IEdmModel GetEdmModel()
    {
        var modelBuilder = new ODataConventionModelBuilder();
        modelBuilder.EnableLowerCamelCase();
        modelBuilder.EntitySet<ModelB>("BModels");
        return modelBuilder.GetEdmModel();
    }
}