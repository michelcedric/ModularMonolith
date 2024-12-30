using Microsoft.AspNetCore.Mvc.Testing;
using My.ModularMonolith.MyAModule.Client.SDK;

namespace My.ModularMonolith.MyAModule.Api.IntegrationTests.Controllers;

public abstract class BaseIntegrationTestsController
{
    protected MyAModuleClient MyAModuleClient { get; private set; }

    protected BaseIntegrationTestsController()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "integrationTest");
        var application = new WebApplicationFactory<My.ModularMonolith.Api.Program>();
        var httpClient = application.CreateClient();
        MyAModuleClient = new MyAModuleClient(httpClient);
    }
}