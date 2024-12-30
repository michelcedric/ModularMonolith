using My.ModularMonolith.Common.Exceptions;
using My.ModularMonolith.MyAModule.Domain.Interfaces.Repositories;

namespace My.ModularMonolith.MyAModule.Api.Application.AModel.Commands.Delete;

public class DeleteAModelCommandHandler(IModelARepository modelARepository) : IRequestHandler<DeleteAModelCommand>
{
    public async Task Handle(DeleteAModelCommand command, CancellationToken cancellationToken)
    {
        var result = await modelARepository.ExecuteDeleteAsync(command.Id, cancellationToken);
        if (!result)
            throw new BusinessValidationException(ErrorCode.ModelANotExists,
                nameof(command.Id), $"Model A with Id : {command.Id} does not exist.");
    }
}