using PortfolioManager.Managers.Tools;
using PortfolioManager.Managers.ToolServices.Interfaces;
using PortfolioManager.Models.Enums;
using PortfolioManager.Models.Results;
using PortfolioManager.Models.ToolModels;

namespace PortfolioManager.Tests;

[TestFixture]
public class PersonalIdentificationNumberValidationServiceTests
{
    private IPersonalIdentificationNumberValidationService personalIdentificationNumberValidationService;

    [SetUp]
    public void Setup()
    {
        personalIdentificationNumberValidationService = new PersonalIdentificationNumberValidationService();
    }

    [Test]
    public void Validate_ShouldWork()
    {
        var result = personalIdentificationNumberValidationService.Validate("850415/123");

        Assert.IsNotNull(result);
        Assert.That(result.Data, Is.TypeOf<PersonalIdentificationNumber>());
        Assert.That(result.Data.IsValid, Is.EqualTo(true));
        Assert.That(result.Data.RawValue, Is.EqualTo("850415/123"));
        Assert.That(result.Data.BirthDate, Is.EqualTo(new DateTime(1885, 4, 15)));
        Assert.That(result.Data.Gender, Is.EqualTo(true));

        var result2 = personalIdentificationNumberValidationService.Validate("905223/4565");

        Assert.IsNotNull(result2);
        Assert.That(result2, Is.TypeOf<DataResult<PersonalIdentificationNumber>>());
        Assert.That(result2.Data.IsValid, Is.EqualTo(true));
        Assert.That(result2.Data.RawValue, Is.EqualTo("905223/4565"));
        Assert.That(result2.Data.BirthDate, Is.EqualTo(new DateTime(1990, 02, 23)));
        Assert.That(result2.Data.Gender, Is.EqualTo(false));
    }

    [Test]
    public void Validate_ShouldNotWork()
    {
        var testInputCollection = new List<string> { null, string.Empty, "   ", "12345", "czch157", "740234/20206" };

        foreach (var testInput in testInputCollection)
        {
            var result = personalIdentificationNumberValidationService.Validate(testInput);

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<DataResult<PersonalIdentificationNumber>>());
            Assert.That(result.Data, Is.Null);
            Assert.That(result.Errors.First(), Is.EqualTo("Invalid Personal Identification Number"));
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.BadRequest));
        }
    }
}
