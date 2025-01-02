using My.ModularMonolith.MyAModule.Client.SDK;
using My.ModularMonolith.MyAModule.Infrastructure.Data;

namespace My.ModularMonolith.MyAModule.Api.IntegrationTests.Controllers.Api;

[TestClass]
public class AModelsControllerTests : BaseIntegrationTestsController
{
    [TestMethod]
    public async Task When_CreateNewAModel_ThenReceivedNewAModel()
    {
        var createCommand = new CreateAModelCommand()
        {
            Name = "Test",
            Description = "Description Test",
        };

        var data = await MyAModuleClient.PostApiAModelsAsync(createCommand);

        Assert.IsNotNull(data);
        Assert.IsNotNull(data.Id);
        Assert.AreEqual(createCommand.Name, data.Name);
        Assert.AreEqual(createCommand.Description, data.Description);
    }

    [TestMethod]
    public async Task When_UpdateAModel_ThenTheCorrectOneCanBeRetrieved()
    {
        var existingData = DbInitializer.AModels.First();

        var updateCommand = new UpdateAModelCommand()
        {
            Id = existingData.Id,
            Name = "Test Update",
            Description = "Description Test Updated",
        };

        await MyAModuleClient.PutApiAModelsAsync(updateCommand);
        var updatedData = await MyAModuleClient.GetOdataAModelskeyAsync(updateCommand.Id);

        Assert.IsNotNull(updatedData);
        Assert.IsNotNull(updatedData.Id);
        Assert.AreEqual(updateCommand.Name, updatedData.Name);
        Assert.AreEqual(updateCommand.Description, updatedData.Description);
    }

    [TestMethod]
    public async Task When_DeleteAModel_ThenSucceed()
    {
        var createCommand = new CreateAModelCommand()
        {
            Name = "Test to delete",
        };

        var data = await MyAModuleClient.PostApiAModelsAsync(createCommand);
        await MyAModuleClient.DeleteApiAModelsAsync(new DeleteAModelCommand() { Id = data.Id });

        var exception = await Assert.ThrowsExceptionAsync<ApiException>(() => MyAModuleClient.GetOdataAModelskeyAsync(data.Id));
        Assert.AreEqual(404, exception.StatusCode);
    }
    
    [TestMethod]
    public async Task When_DeleteUnknownAModel_ThenExceptionRaised()
    {
        var deleteCommand = new DeleteAModelCommand() { Id = Guid.NewGuid() };
        var exception = await Assert.ThrowsExceptionAsync<ApiException<ValidationProblemDetails>>(() =>  MyAModuleClient.DeleteApiAModelsAsync(deleteCommand));
        Assert.AreEqual(400, exception.StatusCode);
    }
}