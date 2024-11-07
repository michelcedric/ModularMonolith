using My.ModularMonolith.Common.Infrastructure.Data.Repositories;
using My.ModularMonolith.MyAModule.Domain.Entities;
using My.ModularMonolith.MyAModule.Domain.Interfaces.Repositories;

namespace My.ModularMonolith.MyAModule.Infrastructure.Data.Repositories;

public class ModelARepository : EfRepository<ModelA, Guid, MyAModuleContext>, IModelARepository
{
    public ModelARepository(MyAModuleContext dbContext) : base(dbContext)
    {
    }
}