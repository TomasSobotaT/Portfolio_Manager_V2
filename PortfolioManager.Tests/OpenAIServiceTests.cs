using Moq;
using PortfolioManager.AI.Repositories.Interfaces;
using PortfolioManager.Managers.Services;
using PortfolioManager.Managers.Services.Interfaces;
using PortfolioManager.Models.Enums;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Tests;

[TestFixture]
public class OpenAIServiceTests
{
    private Mock<IOpenAIRepository> openAIRepositoryMock;
    private IOpenAIService openAIService;

    [SetUp]
    public void Setup()
    {
        openAIRepositoryMock = new Mock<IOpenAIRepository>();
        openAIService = new OpenAIService(openAIRepositoryMock.Object);

    }

    [Test]
    public async Task AskQuestionAsync_ShouldWork()
    {
        openAIRepositoryMock
            .Setup(repo => repo.GenerateAnswerAsync("Hello"))
            .ReturnsAsync("Hi");

        var result = await openAIService.AskQuestionAsync("Hello");

        Assert.IsNotNull(result);
        Assert.That(result, Is.TypeOf<DataResult<string>>());
        Assert.That(result.Data, Is.EqualTo("Hi"));
    }

    [Test]
    public async Task AskQuestionAsync_ShouldNotWork()
    {
        openAIRepositoryMock
            .Setup(repo => repo.GenerateAnswerAsync("Hello"))
            .ThrowsAsync(new Exception("API error"));

        var result = await openAIService.AskQuestionAsync("Hello");

        Assert.That(result, Is.TypeOf<DataResult<string>>());
        Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.BadRequest));
        Assert.That(result.Errors.First(), Is.EqualTo("Error within communication with OpenAI Api : API error"));
    }
}