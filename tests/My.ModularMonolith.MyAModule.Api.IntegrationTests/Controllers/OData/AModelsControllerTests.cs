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
    
    [TestMethod]
    public async Task WhenQueryingForAAModel_ThenAreReturned()
    {
        var expectedData = DbInitializer.AModels.First();
        var data = await MyAModuleClient.GetOdataAModelskeyAsync(expectedData.Id);
        
        Assert.AreEqual(expectedData.Id, data.Id);
        Assert.AreEqual(expectedData.Name, data.Name);
        Assert.AreEqual(expectedData.Description, data.Description);
       
    }
}