using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using My.ModularMonolith.MyAModule.Domain.Entities;

namespace My.ModularMonolith.MyAModule.Infrastructure.Data;

[ExcludeFromCodeCoverage]
public class MyAModuleContext : DbContext
{
    public DbSet<ModelA> ModelAs => Set<ModelA>();

    public MyAModuleContext(DbContextOptions<MyAModuleContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyAModuleContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}