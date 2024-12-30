using System.Text.Json.Serialization;
using Microsoft.AspNetCore.OData;
using Microsoft.FeatureManagement;
using My.ModularMonolith.Api.Extensions;
using My.ModularMonolith.Api.Filters;
using My.ModularMonolith.MyAModule.Infrastructure;
using My.ModularMonolith.MyBModule.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddFeatureManagement();
builder.Services.AddControllers();

builder.AddModuleSwaggerGen();
builder.AddModuleAInfrastructure();
builder.AddModuleBInfrastructure();

builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(GlobalExceptionFilters));
}).ConfigureApplicationPartManager(a =>
{
    a.CleanModulesPart(builder,"My.ModularMonolith.");
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })
    .AddOData(options =>
    {
        options.EnableQueryFeatures();
        options.AddRouteComponents(routePrefix: "MyAModule/odata", model: My.ModularMonolith.MyAModule.Api.Controllers.OData.Builder.GetEdmModel(), actions =>
        {
            actions.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        });
        options.AddRouteComponents(routePrefix: "MyBModule/odata", model: My.ModularMonolith.MyBModule.Api.Controllers.OData.Builder.GetEdmModel(), actions =>
        {
            actions.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        });
    });

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssemblies(
        typeof(My.ModularMonolith.MyAModule.Api.Program).Assembly,
        typeof(My.ModularMonolith.MyBModule.Api.Program).Assembly);
});

var app = builder.Build();

app.UseModuleSwagger(builder.Environment.EnvironmentName);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();