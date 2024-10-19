using Toolbelt;

var solutionDir = FileIO.FindContainerDirToAncestor("Toolbelt.Blazor.LocalTimetext.sln");
var outputPath = Path.Combine(solutionDir, "LocalTimeText", "Internals", "TimeZoneIdMapper.WinTZtoIanaMap.cs");
using var writer = new StreamWriter(outputPath, append: false);
writer.WriteLine("namespace Toolbelt.Blazor.Globalization.Internals;");
writer.WriteLine("");
writer.WriteLine("internal partial class TimeZoneIdMapper");
writer.WriteLine("{");
writer.WriteLine("    private const string WinTzToIanaMap = \"\\r\\n\" +");

var systemTimeZoneIds = TimeZoneInfo.GetSystemTimeZones().Select(tz => tz.Id);
foreach (var systemTimeZoneId in systemTimeZoneIds)
{
    if (!TimeZoneInfo.TryConvertWindowsIdToIanaId(systemTimeZoneId, out var ianaTimeZoneId)) continue;
    writer.WriteLine($"        \"{systemTimeZoneId}\\t{ianaTimeZoneId}\\r\\n\" +");
}

writer.WriteLine("        \"\";");
writer.WriteLine("}");
