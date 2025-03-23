using AutoMapper;
using PortfolioManager.ExternalApis.Clients.Interfaces;
using PortfolioManager.Managers.ToolServices.Interfaces;
using PortfolioManager.Models.Results;
using PortfolioManager.Models.ToolModels;
using System.Text.RegularExpressions;

namespace PortfolioManager.Managers.ToolServices;

public class CompanyIdValidationService(IAresClient aresClient, IMapper mapper) : ICompanyIdValidationService
{
    private readonly List<int> CompanyIdNumberCoeficients = [ 8, 7, 6, 5, 4, 3, 2 ];

    public async Task<DataResult<CompanyId>> ValidateAsync(string text)
    {
        var companyId = new CompanyId
        {
            RawValue = text
        };

        if (string.IsNullOrWhiteSpace(text))
        {
            return new ErrorStatusResult($"Invalid Company Id");
        }

        text = text.Replace(" ", string.Empty).Trim();

        bool isOnlyDigits = Regex.IsMatch(text, @"^\d+$");

        if (!isOnlyDigits || text.Length > 8)
        {
            return new ErrorStatusResult($"Invalid Company Id");
        }

        string normalizedCompanyId = text.PadLeft(8, '0');

        companyId.CompanyIdResult = normalizedCompanyId;

        var isCompanyIdValid = ValidateCompanyIdStructure(normalizedCompanyId);

        if(!isCompanyIdValid)
        {
            return new ErrorStatusResult($"Invalid Company Id");
        }

        companyId.IsValid = true;
        companyId.CompanyIdResult = normalizedCompanyId;

        var aresResult = await aresClient.CheckCompanyIdInAresAsync(normalizedCompanyId);

        if (aresResult is null)
        {
            companyId.IsSubjectValid = false;
            return companyId;
        }

        companyId.IsSubjectValid = true;
        return mapper.Map(aresResult, companyId);
    }

    public bool ValidateCompanyIdStructure(string text)
    {
        int result = 0;

        for (int i = 0; i < text.Length - 1; i++)
        {
            var number = int.Parse(text[i].ToString());
            result += number * CompanyIdNumberCoeficients[i];
        }

        var modulo = result % 11;
        var companyIdControlNumber = int.Parse(text[^1].ToString());

        if (modulo == 0 && companyIdControlNumber == 1)
        {
            return true;
        }

        if (modulo == 1 && companyIdControlNumber == 0)
        {
            return true;
        }

        return companyIdControlNumber == 11 - modulo;

    }
}