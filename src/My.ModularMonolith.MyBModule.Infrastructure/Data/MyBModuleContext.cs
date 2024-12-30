using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using My.ModularMonolith.MyBModule.Domain.Entities;

namespace My.ModularMonolith.MyBModule.Infrastructure.Data;

[ExcludeFromCodeCoverage]
public class MyBModuleContext(DbContextOptions<MyBModuleContext> options) : DbContext(options)
{
    public DbSet<ModelB> ModelAs => Set<ModelB>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyBModuleContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}