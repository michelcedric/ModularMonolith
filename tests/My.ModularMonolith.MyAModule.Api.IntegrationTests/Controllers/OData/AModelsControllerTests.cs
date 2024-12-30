using My.ModularMonolith.MyAModule.Infrastructure.Data;

namespace My.ModularMonolith.MyAModule.Api.IntegrationTests.Controllers.OData;

[TestClass]
public class AModelsControllerTests : BaseIntegrationTestsController
{
    [TestMethod]
    public async Task WhenQueryingForAllAModels_ThenAllAreReturned()
    {
        var response = await MyAModuleClient.GetOdataAModelsAsync();
        Assert.AreEqual(DbInitializer.AModels.Count, response.Count);
    }
}