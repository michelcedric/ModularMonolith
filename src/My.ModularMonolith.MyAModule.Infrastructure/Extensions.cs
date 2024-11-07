using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using My.ModularMonolith.Common.Extensions;
using My.ModularMonolith.MyAModule.Infrastructure.Data;
using Constants = My.ModularMonolith.MyAModule.Infrastructure.Data.Constants;

namespace My.ModularMonolith.MyAModule.Infrastructure;

public static class Extensions
{
    public static void AddModuleAInfrastructure(this IHostApplicationBuilder builder)
    {
        if (builder.AppUseOnlyInMemoryDatabase())
        {
            var name = $"ModuleAContext{Guid.NewGuid()}".Replace("-", "");
            builder.Services.AddDbContext<MyAModuleContext>(options =>
                options.UseInMemoryDatabase(name));
        }
        else
        {
            builder.Services.AddDbContext<MyAModuleContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ModuleAConnection"),
                    s => { s.MigrationsHistoryTable(Constants.Database.MigrationTable, Constants.Database.Schema); }));
        }

        builder.Services.AddRepositories();
    }
}