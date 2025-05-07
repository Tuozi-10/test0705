using System.Globalization;
using System.Text;

public static class TextTools
{
    // Mostly AI generated.
    public static string RemoveAccents(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        string normalized = input.Normalize(NormalizationForm.FormD);

        var sb = new StringBuilder();
        
        foreach (char c in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        }
        
        return sb.ToString().Normalize(NormalizationForm.FormC);
    }
}