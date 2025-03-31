namespace PortfolioManager.Models.ToolModels;

public class CompanyIdModel
{
    public string RawValue { get; init; }

    public bool IsValid { get; set; }

    public bool IsSubjectValid { get; set; }

    public string CompanyName { get; set; }

    public string CompanyIdResult { get; set; }

    public string VatId { get; set; }

    public string CompanyAddress { get; set; }
}
