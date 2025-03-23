using PortfolioManager.Managers.ToolServices.Interfaces;
using PortfolioManager.Models.Results;
using PortfolioManager.Models.ToolModels;
using System.Text.RegularExpressions;

namespace PortfolioManager.Managers.Tools;

public class PersonalIdentificationNumberValidationService : IPersonalIdentificationNumberValidationService
{
    public DataResult<PersonalIdentificationNumber> Validate(string text)
    {
        var personalIdentificationNumber = new PersonalIdentificationNumber
        {
            RawValue = text
        };

        if (string.IsNullOrWhiteSpace(text))
        {
            return new ErrorStatusResult($"Invalid Personal Identification Number");
        }

        text = Regex.Replace(text, @"\D", string.Empty);

        if (text.Length < 9 || text.Length > 10)
        {
            return new ErrorStatusResult($"Invalid Personal Identification Number");
        }

        var year = int.Parse(text[..2]);

        if (text.Length == 9)
        {
            year += year < 54 ? 1900 : 1800;
        }
        else
        {
            year += year < 54 ? 2000 : 1900;
        }
        var month = int.Parse(text[2..4]);

        if (month >= 1 && month <= 12)
        {
            personalIdentificationNumber.Gender = true;
            personalIdentificationNumber.IsExtraSequence = false;
        }
        else if (month >= 51 && month <= 62)
        {
            personalIdentificationNumber.Gender = false;
            personalIdentificationNumber.IsExtraSequence = false;
            month -= 50;
        }
        else if (month >= 21 && month <= 32)
        {
            personalIdentificationNumber.Gender = true;
            personalIdentificationNumber.IsExtraSequence = true;
            month -= 20;
        }
        else if (month >= 61 && month <= 72)
        {
            personalIdentificationNumber.Gender = false;
            personalIdentificationNumber.IsExtraSequence = true;
            month -= 20;
        }
        else
        {
            return new ErrorStatusResult($"Invalid Personal Identification Number");
        }

        if (!DateTime.TryParse($"{year}-{month}-{text[4..6]}", out var date))
        {
            return new ErrorStatusResult($"Invalid Personal Identification Number");
        }

        personalIdentificationNumber.BirthDate = date;

        personalIdentificationNumber.SequenceNumber = int.Parse(text[6..9]);

        if (year >= 1954 && year < 2022 && long.Parse(text) % 11 > 0)
        {
            return new ErrorStatusResult($"Invalid Personal Identification Number");
        }

        personalIdentificationNumber.IsValid = true;
        return personalIdentificationNumber;
    }
}
