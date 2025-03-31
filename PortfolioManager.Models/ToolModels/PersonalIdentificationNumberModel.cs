namespace PortfolioManager.Models.ToolModels;

public class PersonalIdentificationNumberModel
{
    public string RawValue { get; init; }

    public DateTime? BirthDate { get; set; }

    public int? SequenceNumber { get; set; }

    public bool? Gender { get; set; }

    public bool? IsExtraSequence { get; set; }

    public bool IsValid { get; set; }
}
