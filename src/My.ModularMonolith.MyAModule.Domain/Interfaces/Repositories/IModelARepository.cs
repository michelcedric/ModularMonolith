using My.ModularMonolith.Common.Domain.Interfaces.Repositories;
using My.ModularMonolith.MyAModule.Domain.Entities;

namespace My.ModularMonolith.MyAModule.Domain.Interfaces.Repositories;

public interface IModelARepository : IAsyncRepository<ModelA, Guid>;