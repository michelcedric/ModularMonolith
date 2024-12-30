using My.ModularMonolith.Common.Infrastructure.Data.Repositories;
using My.ModularMonolith.MyAModule.Domain.Entities;
using My.ModularMonolith.MyAModule.Domain.Interfaces.Repositories;

namespace My.ModularMonolith.MyAModule.Infrastructure.Data.Repositories;

public class ModelARepository(MyAModuleContext dbContext)
    : EfRepository<ModelA, Guid, MyAModuleContext>(dbContext), IModelARepository;