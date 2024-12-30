using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using My.ModularMonolith.Common.Extensions;
using My.ModularMonolith.MyBModule.Infrastructure.Data;
using Constants = My.ModularMonolith.MyBModule.Infrastructure.Data.Constants;

namespace My.ModularMonolith.MyBModule.Infrastructure;

public static class Extensions
{
    public static void AddModuleBInfrastructure(this IHostApplicationBuilder builder)
    {
        if (builder.AppUseOnlyInMemoryDatabase())
        {
            var name = $"ModuleBContext{Guid.NewGuid()}".Replace("-", "");
            builder.Services.AddDbContext<MyBModuleContext>(options =>
                options.UseInMemoryDatabase(name));
        }
        else
        {
            builder.Services.AddDbContext<MyBModuleContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ModuleBDatabaseConnection"),
                    s => { s.MigrationsHistoryTable(Constants.Database.MigrationTable, Constants.Database.Schema); }));
        }

        builder.Services.AddRepositories();
    }
    
    public static async Task ApplyModuleBSeedData(this IHost app, IHostApplicationBuilder builder)
    {
        if (builder.AppSeedOnStartup())
        {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MyBModuleContext>();
                await DbInitializer.Run(context);
            }
        }
    }
}