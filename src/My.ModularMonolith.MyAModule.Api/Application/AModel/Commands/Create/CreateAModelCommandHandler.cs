using My.ModularMonolith.MyAModule.Domain.Entities;
using My.ModularMonolith.MyAModule.Domain.Interfaces.Repositories;

namespace My.ModularMonolith.MyAModule.Api.Application.AModel.Commands.Create;

public class CreateAModelCommandHandler(IModelARepository repository) : IRequestHandler<CreateAModelCommand, ModelA>
{
    public async Task<ModelA> Handle(CreateAModelCommand command, CancellationToken cancellationToken)
    {
            var data = ModelA.Create(command.Name, command.Description);
            return await repository.AddAsync(data, cancellationToken);
    }
}