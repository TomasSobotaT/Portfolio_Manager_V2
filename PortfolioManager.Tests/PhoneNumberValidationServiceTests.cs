using AutoFixture;
using PortfolioManager.Managers.Tools;
using PortfolioManager.Managers.Tools.Interfaces;

namespace PortfolioManager.Tests;

[TestFixture]
public class PhoneNumberValidationServiceTests
{
    private IPhoneNumberValidationService phoneNumberValidationService;
    private Fixture fixture;

    [SetUp]
    public void Setup()
    {
        phoneNumberValidationService = new PhoneNumberValidationService();
        fixture = new Fixture();
        fixture.Customizations.Add(new RandomNumericSequenceGenerator(100_000_000, 999_999_999));
    }

    [Test]
    public void Validate_ShouldWork()
    {
        int fakePhoneNumber = fixture.Create<int>();
        var result = phoneNumberValidationService.Validate(fakePhoneNumber.ToString(), Base.Enums.Countries.CZ);
        Assert.That(result.Data.IsValid, Is.EqualTo(true));
    }
}
