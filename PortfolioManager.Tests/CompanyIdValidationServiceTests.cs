using AutoMapper;
using Moq;
using NUnit.Framework.Internal;
using PortfolioManager.ExternalApis.Clients.Interfaces;
using PortfolioManager.Managers.ToolServices;
using PortfolioManager.Managers.ToolServices.Interfaces;
using PortfolioManager.Models.Responses;
using PortfolioManager.Models.ToolModels;

namespace PortfolioManager.Tests;

[TestFixture]
public class CompanyIdValidationServiceTests
{
    private ICompanyIdValidationService companyIdValidationService;
    private Mock<IAresClient> aresClientMock;
    private Mock<IMapper> mapperMock;

    [SetUp]
    public void Setup()
    {
        aresClientMock = new Mock<IAresClient>();
        mapperMock = new Mock<IMapper>();
        companyIdValidationService = new CompanyIdValidationService(aresClientMock.Object, mapperMock.Object);
    }

    [Test]
    public async Task Validate_ShouldWork()
    {
        var aresEconomicSubjectResponseTest = GetAresEconomicSubjectResponseTest();
        var companyIdTest = GetClientIdTest();

        aresClientMock.
            Setup(client => client
            .CheckCompanyIdInAresAsync("64945880"))
                .ReturnsAsync(aresEconomicSubjectResponseTest);

        mapperMock
            .Setup(map => map.Map(
                It.IsAny<AresEconomicSubjectResponse>(),
                It.IsAny<CompanyIdModel>()))
            .Returns((AresEconomicSubjectResponse source, CompanyIdModel dest) =>
            {
                dest.IsValid = true;
                dest.CompanyName = source.CompanyName;
                dest.VatId = source.Vatid;
                dest.CompanyAddress = source.CompanyAddress?.Address;
                return dest;
            });

        var result = await companyIdValidationService.ValidateAsync("64945880");

        Assert.IsNotNull(result);
        Assert.That(result.Data, Is.TypeOf<CompanyIdModel>());
        Assert.That(result.Data.IsValid, Is.EqualTo(true));
        Assert.That(result.Data.RawValue, Is.EqualTo("64945880"));
        Assert.That(result.Data.CompanyAddress, Is.EqualTo("TestCompanyAddress"));
        Assert.That(result.Data.CompanyIdResult, Is.EqualTo("64945880"));
        Assert.That(result.Data.VatId, Is.EqualTo("TestCompanyVatId"));
        Assert.That(result.Data.CompanyName, Is.EqualTo("TestCompanyName"));
    }

    private AresEconomicSubjectResponse GetAresEconomicSubjectResponseTest()
    {
        return new AresEconomicSubjectResponse
        {
            CompanyId = "TestCompanyId",
            CompanyName = "TestCompanyName",
            Vatid = "TestCompanyVatId",
            CompanyAddress = new CompanyAddress { Address = "TestCompanyAddress" },
        };
    }

    private CompanyIdModel GetClientIdTest()
    {
        return new CompanyIdModel
        {
            CompanyIdResult = "TestCompanyId",
            CompanyName = "TestCompanyName",
            VatId = "TestCompanyVatId",
            CompanyAddress = "TestCompanyAddress",
        };
    }

}
