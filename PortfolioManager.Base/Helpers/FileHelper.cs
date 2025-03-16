using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace PortfolioManager.Base.Helpers;
public static class FileHelper
{
    public static string NormalizeFileName(string fileName)
    {
        string[] ReservedNames = ["^CON$", "^PRN$", "^AUX$", "^NUL$", @"^COM\d$", @"^LPT\d$"];
        string ValidFileNameChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.";

        fileName = Path.GetFileName(fileName);
        fileName = fileName.Replace(' ', '_');
        fileName = string.Join("", fileName.Normalize(NormalizationForm.FormD).Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)).Normalize(NormalizationForm.FormC);
        fileName = string.Join("", fileName.Where(c => ValidFileNameChars.Contains(c)));

        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

        var extension = Path.GetExtension(fileName);

        if (fileNameWithoutExtension.Length > 32)
        {
            fileNameWithoutExtension = fileNameWithoutExtension[..32];
        }

        if (extension.Length > 12)
        {
            extension = extension[..12];
        }

        fileNameWithoutExtension = fileNameWithoutExtension.Replace('.', '_');

        if (string.IsNullOrWhiteSpace(fileNameWithoutExtension) || string.IsNullOrWhiteSpace(extension))
        {
            return null;
        }

        foreach (var reservedName in ReservedNames)
        {
            if (Regex.IsMatch(fileNameWithoutExtension, reservedName, RegexOptions.IgnoreCase))
            {
                return null;
            }
        }

        return string.Concat(fileNameWithoutExtension, extension);
    }
}
