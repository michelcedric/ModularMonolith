using My.ModularMonolith.Common.Domain.Interfaces.Repositories;
using My.ModularMonolith.MyBModule.Domain.Entities;

namespace My.ModularMonolith.MyBModule.Domain.Interfaces.Repositories;

public interface IModelBRepository : IAsyncRepository<ModelB, Guid>;