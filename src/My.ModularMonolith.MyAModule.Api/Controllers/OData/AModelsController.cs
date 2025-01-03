using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.FeatureManagement.Mvc;
using My.ModularMonolith.MyAModule.Domain.Entities;
using My.ModularMonolith.MyAModule.Domain.Interfaces.Repositories;

namespace My.ModularMonolith.MyAModule.Api.Controllers.OData;

[FeatureGate(Constants.Api.ModuleName)]
[EnableQuery]
public class AModelsController(IModelARepository modelARepository) : ODataController
{
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ModelA), 200)]
    public IActionResult Get([FromRoute] Guid key)
    {
        var query = modelARepository.GetAsQueryable(e => e.Id == key);
        return Ok(SingleResult.Create(query));
    }

    public ActionResult<IQueryable<ModelA>> Get()
    {
        return Ok(modelARepository.GetAllAsQueryable());
    }
}