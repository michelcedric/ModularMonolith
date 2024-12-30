using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using My.ModularMonolith.MyAModule.Domain.Entities;

namespace My.ModularMonolith.MyAModule.Infrastructure.Data;

[ExcludeFromCodeCoverage]
public class MyAModuleContext(DbContextOptions<MyAModuleContext> options) : DbContext(options)
{
    public DbSet<ModelA> ModelAs => Set<ModelA>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyAModuleContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}