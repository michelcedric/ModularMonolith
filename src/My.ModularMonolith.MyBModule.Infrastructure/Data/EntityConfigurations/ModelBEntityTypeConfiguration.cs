using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My.ModularMonolith.MyBModule.Domain.Entities;

namespace My.ModularMonolith.MyBModule.Infrastructure.Data.EntityConfigurations;

public class ModelBEntityTypeConfiguration : IEntityTypeConfiguration<ModelB>
{
    public void Configure(EntityTypeBuilder<ModelB> builder)
    {
        builder.ToTable("ModelBs", Constants.Database.Schema);
        builder.HasKey(x => x.Id);
    }
}