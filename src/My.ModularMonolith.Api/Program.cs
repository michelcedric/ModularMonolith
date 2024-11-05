using Microsoft.FeatureManagement;
using My.ModularMonolith.Api.Extensions;
using My.ModularMonolith.Api.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddFeatureManagement();
builder.Services.AddControllers();

builder.AddModuleSwaggerGen();

builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(GlobalExceptionFilters));
}).ConfigureApplicationPartManager(a =>
{
    a.CleanModulesPart(builder,"My.ModularMonolith.");
});
var app = builder.Build();

app.UseModuleSwagger(builder.Environment.EnvironmentName);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();