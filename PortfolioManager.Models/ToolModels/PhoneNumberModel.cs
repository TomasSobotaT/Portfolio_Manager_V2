namespace PortfolioManager.Models.ToolModels;

public class PhoneNumberModel
{
    public string RawValue { get; init; }

    public string E164Format { get; set; }

    public string InternationalFormat { get; set; }

    public string NationalFormat { get; set; }

    public string Region { get; set; }

    public string NumberType { get; set; }

    public bool IsValid { get; set; }

    public string Message { get; set; }
}
