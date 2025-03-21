using System.Text.RegularExpressions;

namespace PortfolioManager.Models.Models.PersonalIdentificationNumber;

public class PersonalIdentificationNumber
{
    private string rawValue;

    public DateTime BirthDate { get; private set; }

    public int SequenceNumber { get; private set; }

    public bool Gender { get; private set; }

    public bool IsExtraSequence { get; private set; }

    public static bool TryParse(string text, out PersonalIdentificationNumber personalIdentificationNumber)
    {
        personalIdentificationNumber = null;

        if (string.IsNullOrWhiteSpace(text))
        {
            return false;
        }

        text = Regex.Replace(text, @"\D", string.Empty);

        if (text.Length < 9 || text.Length > 10)
        {
            return false;
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
            return false;
        }

        var date = DateTime.TryParse()

        personalIdentificationNumber.BirthDate = new DateTime(year, month, int.Parse(text[4..6]));

        return true;
    }
}
