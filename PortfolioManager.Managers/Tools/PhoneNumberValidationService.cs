using PhoneNumbers;
using PortfolioManager.Base.Enums;
using PortfolioManager.Managers.Tools.Interfaces;
using PortfolioManager.Models.Results;
using PortfolioManager.Models.ToolModels;
using System.Globalization;

namespace PortfolioManager.Managers.Tools;

public class PhoneNumberValidationService : IPhoneNumberValidationService
{
    public DataResult<PhoneNumberModel> Validate(string text, Countries country)
    {
        var phoneNumberModel = new PhoneNumberModel
        {
            RawValue = text
        };

        if (string.IsNullOrWhiteSpace(text))
        {
            phoneNumberModel.IsValid = false;
            phoneNumberModel.Message = "Invalid phone number format.";
            return phoneNumberModel;
        }

        var phoneNumberUtilInstance = PhoneNumberUtil.GetInstance();

        try
        {
            var regionCode = country.ToString();
            var phoneNumberInstance = phoneNumberUtilInstance.Parse(text, regionCode);

            if (!phoneNumberUtilInstance.IsValidNumberForRegion(phoneNumberInstance, regionCode))
            {
                phoneNumberModel.IsValid = false;
                phoneNumberModel.Message = "The phone number is not valid for the specified country.";

                return phoneNumberModel;
            }

            var numberType = phoneNumberUtilInstance.GetNumberType(phoneNumberInstance);
            var regionType = phoneNumberUtilInstance.GetRegionCodeForNumber(phoneNumberInstance);

            phoneNumberModel.IsValid = true;
            phoneNumberModel.Message = "The phone number is valid.";
            phoneNumberModel.E164Format = phoneNumberUtilInstance.Format(phoneNumberInstance, PhoneNumberFormat.E164);
            phoneNumberModel.InternationalFormat = phoneNumberUtilInstance.Format(phoneNumberInstance, PhoneNumberFormat.INTERNATIONAL);
            phoneNumberModel.NationalFormat = phoneNumberUtilInstance.Format(phoneNumberInstance, PhoneNumberFormat.NATIONAL);
            phoneNumberModel.NumberType = GetNumberType(numberType);
            phoneNumberModel.Region = new RegionInfo(regionType).EnglishName;

            return phoneNumberModel;

        }
        catch (NumberParseException)
        {
            phoneNumberModel.IsValid = false;
            phoneNumberModel.Message = "Invalid phone number format.";

            return phoneNumberModel;
        }
    }

    private static string GetNumberType(PhoneNumberType numberType)
    {
        return numberType switch
        {
            PhoneNumberType.MOBILE => "Mobile",
            PhoneNumberType.FIXED_LINE => "Fixed line",
            PhoneNumberType.TOLL_FREE => "Toll-free",
            PhoneNumberType.PREMIUM_RATE => "Premium rate",
            PhoneNumberType.SHARED_COST => "Shared cost",
            PhoneNumberType.VOIP => "VoIP",
            PhoneNumberType.PERSONAL_NUMBER => "Personal number",
            PhoneNumberType.PAGER => "Pager",
            PhoneNumberType.UAN => "UAN",
            PhoneNumberType.VOICEMAIL => "Voicemail",
            PhoneNumberType.FIXED_LINE_OR_MOBILE => "Fixed line or Mobile",
            _ => "Unknown"
        };
    }
}

