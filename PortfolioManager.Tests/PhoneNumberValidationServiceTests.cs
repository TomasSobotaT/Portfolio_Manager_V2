using AutoFixture;
using AutoFixture.Kernel;
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
    }

    [Test]
    public void Validate_CzTest()
    {
        var generator = new RandomNumericSequenceGenerator(100_000_000, 999_999_999);
        var phoneNumber = (int)generator.Create(typeof(int), new SpecimenContext(fixture));
        var result = phoneNumberValidationService.Validate(phoneNumber.ToString(), Base.Enums.Countries.CZ);

        if (result.Data.IsValid)
        {
            Assert.IsTrue(result.Data.IsValid);
            Assert.That(result.Data.E164Format[0..4], Is.EqualTo("+420"));
            Assert.That(result.Data.E164Format.Length, Is.EqualTo(13));
            Assert.That(result.Data.NationalFormat.Length, Is.EqualTo(11));
            Assert.That(result.Data.Region, Is.EqualTo("Czechia"));
            Assert.That(result.Data.Message, Is.EqualTo("The phone number is valid."));
        }
        else
        {
            Assert.IsFalse(result.Data.IsValid);
            Assert.That(result.Data.InternationalFormat, Is.Null);
            Assert.That(result.Data.E164Format, Is.Null);
            Assert.That(result.Data.NationalFormat, Is.Null);
        }
    }

    [Test]
    public void Validate_CzShouldWork()
    {
        var phoneNumbers = new List<string>(["420123456789", "732874569", "+420987654321", "383985412", "+420383741741"]);

        var phoneNumber = phoneNumbers[Random.Shared.Next(5)];

        var result = phoneNumberValidationService.Validate(phoneNumber.ToString(), Base.Enums.Countries.CZ);

        Assert.IsTrue(result.Data.IsValid);
        Assert.That(result.Data.E164Format, Is.EqualTo($"+420{phoneNumber}"));
        Assert.That(result.Data.E164Format.Length, Is.EqualTo(13));
        Assert.That(result.Data.NationalFormat.Length, Is.EqualTo(11));
        Assert.That(result.Data.Region, Is.EqualTo("Czechia"));
        Assert.That(result.Data.Message, Is.EqualTo("The phone number is valid."));
     }

    [Test]
    public void Validate_CzShoulNotWork()
    {
        var phoneNumbers = new List<string>(["420s23456789", "1732874569", "+4209876543210", "383985412FF", "+42038rR41741"]);

        var phoneNumber = phoneNumbers[Random.Shared.Next(5)];

        var result = phoneNumberValidationService.Validate(phoneNumber.ToString(), Base.Enums.Countries.CZ);

        Assert.IsFalse(result.Data.IsValid);
        Assert.IsNull(result.Data.E164Format);
        Assert.IsNull(result.Data.NationalFormat);
        Assert.IsNull(result.Data.InternationalFormat);
    }//test
}
