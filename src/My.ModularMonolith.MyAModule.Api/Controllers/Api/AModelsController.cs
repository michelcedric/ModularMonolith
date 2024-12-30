using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using My.ModularMonolith.MyAModule.Api.Application.AModel.Commands.Delete;

namespace My.ModularMonolith.MyAModule.Api.Controllers.Api;

[ApiController]
[FeatureGate(Constants.Api.ModuleName)]
[ApiExplorerSettings(GroupName = Constants.Api.GroupName)]
[Route("MyAModule/api/AModels")]
public class AModelsController(ref readonly IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    
    [HttpDelete]
    [ProducesResponseType(typeof(void), 204)]
    public async Task<IActionResult> Delete(DeleteAModelCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}
