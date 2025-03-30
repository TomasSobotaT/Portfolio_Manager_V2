using DocumentFormat.OpenXml.Packaging;
using OfficeOpenXml;
using System.Text;


namespace PortfolioManager.Base.Helpers;

public static class DocumentContentExtractHelper
{
    public static string ExtractTextFromDocument(byte[] fileData, string contentType)
    {
        return contentType.ToLowerInvariant() switch
        {
            "application/pdf" => ExtractFromPdf(fileData),
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document" => ExtractFromWord(fileData),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" => ExtractFromExcel(fileData),
            _ => ExtractFromTxt(fileData),
        };
    }

    private static string ExtractFromPdf(byte[] data)
    {
        using var stream = new MemoryStream(data);
        using var pdf = UglyToad.PdfPig.PdfDocument.Open(stream);
        var sb = new StringBuilder();

        foreach (var page in pdf.GetPages())
        {
            sb.AppendLine(page.Text);
        }

        return sb.ToString();
    }

    private static string ExtractFromWord(byte[] data)
    {
        using var stream = new MemoryStream(data);
        using var doc = WordprocessingDocument.Open(stream, false);

        return doc.MainDocumentPart.Document.Body.InnerText;
    }

    private static string ExtractFromExcel(byte[] data)
    {
        ExcelPackage.License.SetNonCommercialPersonal("MyName");

        using var stream = new MemoryStream(data);
        using var package = new ExcelPackage(stream);
        var sb = new StringBuilder();

        foreach (var sheet in package.Workbook.Worksheets)
        {
            var rowCount = sheet.Dimension?.Rows ?? 0;
            var colCount = sheet.Dimension?.Columns ?? 0;

            for (int r = 1; r <= rowCount; r++)
                for (int c = 1; c <= colCount; c++)
                    sb.Append($"{sheet.Cells[r, c].Text} ");
        }

        return sb.ToString();
    }

    private static string ExtractFromTxt(byte[] data)
    {
        return Encoding.UTF8.GetString(data);
    }
}
