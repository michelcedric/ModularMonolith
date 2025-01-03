using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using My.ModularMonolith.Common.Domain.Entities;
using My.ModularMonolith.MyAModule.Domain.Entities;

namespace My.ModularMonolith.MyAModule.Api.Controllers.OData;

public static class Builder
{
    public static IEdmModel GetEdmModel()
    {
        var modelBuilder = new ODataConventionModelBuilder();
        modelBuilder.EntityType<BaseEntity<Guid>>();
        modelBuilder.EnableLowerCamelCase();
        modelBuilder.EntitySet<ModelA>("AModels");
        return modelBuilder.GetEdmModel();
    }
}