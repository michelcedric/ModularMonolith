using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.FeatureManagement.Mvc;
using My.ModularMonolith.MyBModule.Domain.Entities;
using My.ModularMonolith.MyBModule.Domain.Interfaces.Repositories;

namespace My.ModularMonolith.MyBModule.Api.Controllers.OData;

[FeatureGate(Constants.Api.ModuleName)]
[EnableQuery]
public class BModelsController(ref readonly IModelBRepository modelBRepository) : ODataController
{
    private readonly IModelBRepository _modelBRepository = modelBRepository;

    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ModelB), 200)]
    public IActionResult Get([FromRoute] Guid key)
    {
        var query = _modelBRepository.GetAsQueryable(e => e.Id == key);
        return Ok(SingleResult.Create(query));
    }

    public ActionResult<IQueryable<ModelB>> Get()
    {
        return Ok(_modelBRepository.GetAllAsQueryable());
    }
}