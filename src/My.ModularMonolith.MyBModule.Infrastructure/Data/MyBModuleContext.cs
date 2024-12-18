using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using My.ModularMonolith.MyBModule.Domain.Entities;

namespace My.ModularMonolith.MyBModule.Infrastructure.Data;

[ExcludeFromCodeCoverage]
public class MyBModuleContext : DbContext
{
    public DbSet<ModelB> ModelAs => Set<ModelB>();

    public MyBModuleContext(DbContextOptions<MyBModuleContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyBModuleContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}