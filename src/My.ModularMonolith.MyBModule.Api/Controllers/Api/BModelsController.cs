using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using My.ModularMonolith.MyBModule.Api.Application.BModel.Commands.Delete;

namespace My.ModularMonolith.MyBModule.Api.Controllers.Api;

[ApiController]
[FeatureGate(Constants.Api.ModuleName)]
[ApiExplorerSettings(GroupName = Constants.Api.GroupName)]
[Route("MyBModule/api/BModels")]
public class BModelsController(ref readonly IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpDelete]
    [ProducesResponseType(typeof(void), 204)]
    public async Task<IActionResult> Delete(DeleteBModelCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}