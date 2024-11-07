using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My.ModularMonolith.MyAModule.Domain.Entities;

namespace My.ModularMonolith.MyAModule.Infrastructure.Data.EntityConfigurations;

public class ModelAEntityTypeConfiguration : IEntityTypeConfiguration<ModelA>
{
    public void Configure(EntityTypeBuilder<ModelA> builder)
    {
        builder.ToTable("ModelAs", Constants.Database.Schema);
    }
}