using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Toolbelt;

var solutionDir = FileIO.FindContainerDirToAncestor("Toolbelt.Blazor.LocalTimetext.sln");
var outputPath = Path.Combine(solutionDir, "LocalTimeText", "Internals", "TimeZoneIdMapper.TzAbbreviationToIanaMap.cs");
using var writer = new StreamWriter(outputPath, append: false);
writer.WriteLine("namespace Toolbelt.Blazor.Globalization.Internals;");
writer.WriteLine("");
writer.WriteLine("internal partial class TimeZoneIdMapper");
writer.WriteLine("{");
writer.WriteLine("    private const string TzAbbreviationToIanaMap = \"\\r\\n\" +");

var displayNameToIanaId = TimeZoneInfo.GetSystemTimeZones().ToDictionary(
    winTz => winTz.DisplayName,
    winTz => TimeZoneInfo.TryConvertWindowsIdToIanaId(winTz.Id, out var ianaId) ? ianaId : null);

var map = new Dictionary<string, string>();

foreach (var (abbreviation, ianaId) in EnumareteTzAbbreviationToIanaPair(map))
{
    map.Add(abbreviation, ianaId);
    writer.WriteLine($"        \"{abbreviation}\\t{ianaId}\\r\\n\" +");
}

writer.WriteLine("        \"\";");
writer.WriteLine("}");


IEnumerable<(string abbreviation, string ianaId)> EnumareteTzAbbreviationToIanaPair(Dictionary<string, string> map)
{
    foreach (var tzAbbreviation in TzAbbreviations.All.Order().Distinct())
    {
        var match = Regex.Match(tzAbbreviation, @"^(?<commonName>[A-Z]+)[ ]*\((?<standardName>[A-Z]+)/(?<daylightName>[A-Z]+)\)$");
        if (match.Success)
        {
            var standardName = match.Groups["standardName"].Value;
            var daylightName = match.Groups["daylightName"].Value;
            var commonName = match.Groups["commonName"].Value;

            if (map.TryGetValue(standardName, out var ianaTzId0))
            {
                yield return (daylightName, ianaTzId0);
                yield return (commonName, ianaTzId0);
            }
            else
            {
                if (TryToConvertToIanaId(daylightName, out var ianaTzId1)) yield return (daylightName, ianaTzId1);
                if (TryToConvertToIanaId(commonName, out var ianaTzId2)) yield return (commonName, ianaTzId2);
            }
        }
        else if (TryToConvertToIanaId(tzAbbreviation, out var ianaTzId3))
        {
            yield return (tzAbbreviation, ianaTzId3);
        }
    }

}


bool TryToConvertToIanaId(string tzAbbreviation, [NotNullWhen(true)] out string? ianaTimeZoneId)
{
    ianaTimeZoneId = null;
    if (!TimeZoneInfo.TryFindSystemTimeZoneById(tzAbbreviation, out var tz)) return false;
    if (!displayNameToIanaId.TryGetValue(tz.DisplayName, out ianaTimeZoneId)) return false;
    if (ianaTimeZoneId is null) return false;
    return true;
}