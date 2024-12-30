using My.ModularMonolith.Common.Infrastructure.Data.Repositories;
using My.ModularMonolith.MyBModule.Domain.Entities;
using My.ModularMonolith.MyBModule.Domain.Interfaces.Repositories;

namespace My.ModularMonolith.MyBModule.Infrastructure.Data.Repositories;

public class ModelBRepository(MyBModuleContext dbContext)
    : EfRepository<ModelB, Guid, MyBModuleContext>(dbContext), IModelBRepository;