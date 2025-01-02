using My.ModularMonolith.Common.Exceptions;
using My.ModularMonolith.MyAModule.Domain.Interfaces.Repositories;

namespace My.ModularMonolith.MyAModule.Api.Application.AModel.Commands.Update;

public class UpdateAModelCommandHandler(IModelARepository repository) : IRequestHandler<UpdateAModelCommand>
{
    public async Task Handle(UpdateAModelCommand command, CancellationToken cancellationToken)
    {
        var data = await repository.GetByIdAsync(command.Id, cancellationToken);
        if (data == null)
        {
            throw new BusinessValidationException(ErrorCode.ModelANotExists,
                nameof(command.Id), $"Model A with Id : {command.Id} does not exist.");
        }
        data.Update(command.Name, command.Description);
        await repository.UpdateAsync(data, cancellationToken);
    }
}