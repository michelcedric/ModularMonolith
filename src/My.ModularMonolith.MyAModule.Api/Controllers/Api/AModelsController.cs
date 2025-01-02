using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using My.ModularMonolith.MyAModule.Api.Application.AModel.Commands.Create;
using My.ModularMonolith.MyAModule.Api.Application.AModel.Commands.Delete;
using My.ModularMonolith.MyAModule.Api.Application.AModel.Commands.Update;
using My.ModularMonolith.MyAModule.Domain.Entities;

namespace My.ModularMonolith.MyAModule.Api.Controllers.Api;

[ApiController]
[FeatureGate(Constants.Api.ModuleName)]
[ApiExplorerSettings(GroupName = Constants.Api.GroupName)]
[Route("MyAModule/api/AModels")]
public class AModelsController(IMediator mediator) : ControllerBase
{
    [HttpDelete]
    [ProducesResponseType(typeof(void), 204)]
    public async Task<IActionResult> Delete(DeleteAModelCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(typeof(ModelA), 201)]
    public async Task<IActionResult> Create(CreateAModelCommand command)
    {
        var result = await mediator.Send(command);
        return Created("AModels", result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(void), 204)]
    public async Task<IActionResult> Update(UpdateAModelCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}