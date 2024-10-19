namespace Toolbelt.Blazor.Globalization.Internals;

/// <summary>
/// Provides methods for converting time zone IDs to their corresponding IANA IDs.
/// </summary>
internal partial class TimeZoneIdMapper : ITimeZoneIdMapper
{
    /// <summary>
    /// Tries to convert the given time zone ID to its corresponding IANA ID using the provided map.
    /// </summary>
    /// <param name="tzId">The time zone ID to convert.</param>
    /// <param name="map">The map containing the conversion data.</param>
    /// <param name="ianaId">The corresponding IANA ID if the conversion is successful; otherwise, an empty string.</param>
    /// <returns><c>true</c> if the conversion is successful; otherwise, <c>false</c>.</returns>
    private static bool TryConvertToIanaId(string tzId, string map, out string ianaId)
    {
        ianaId = "";
        var searchTerm = "\n" + tzId + "\t";
        var indexOfStart = map.IndexOf(searchTerm);
        if (indexOfStart < 0) return false;

        indexOfStart += searchTerm.Length;
        var indexOfEnd = map.IndexOf("\r", indexOfStart);
        ianaId = map.Substring(indexOfStart, indexOfEnd - indexOfStart);
        return true;
    }

    /// <summary>
    /// Tries to convert the given Windows time zone ID to its corresponding IANA ID.
    /// </summary>
    /// <param name="winTzId">The Windows time zone ID to convert, like "Pacific Standard Time".</param>
    /// <param name="ianaId">The corresponding IANA ID if the conversion is successful, like "America/Los_Angeles"; otherwise, an empty string.</param>
    /// <returns><c>true</c> if the conversion is successful; otherwise, <c>false</c>.</returns>
    public bool TryConvertWinTzToIanaId(string winTzId, out string ianaId) => TryConvertToIanaId(winTzId, WinTzToIanaMap, out ianaId);

    /// <summary>
    /// Tries to convert the given time zone abbreviation to its corresponding IANA ID.
    /// </summary>
    /// <param name="tzAbbreviation">The time zone abbreviation to convert, like "PST".</param>
    /// <param name="ianaId">The corresponding IANA ID if the conversion is successful, like "America/Los_Angeles"; otherwise, an empty string.</param>
    /// <returns><c>true</c> if the conversion is successful; otherwise, <c>false</c>.</returns>
    public bool TryConvertTzAbbreviationToIanaId(string tzAbbreviation, out string ianaId) => TryConvertToIanaId(tzAbbreviation, TzAbbreviationToIanaMap, out ianaId);


    /// <summary>
    /// Converts the given time zone ID to a corresponding TimeZoneInfo object.
    /// </summary>
    /// <param name="timeZoneId">The time zone ID to convert.</param>
    /// <returns>The corresponding TimeZoneInfo object.</returns>
    public TimeZoneInfo TimeZoneIdToTimeZoneInfo(string? timeZoneId)
    {
        return timeZoneId switch
        {
            null => TimeZoneInfo.Utc,
            _ when this.TryConvertWinTzToIanaId(timeZoneId, out var ianaId) && TimeZoneInfo.TryFindSystemTimeZoneById(ianaId, out var tz) => tz,
            _ when TimeZoneInfo.TryFindSystemTimeZoneById(timeZoneId, out var tz) => tz,
            _ when this.TryConvertTzAbbreviationToIanaId(timeZoneId, out var ianaId) && TimeZoneInfo.TryFindSystemTimeZoneById(ianaId, out var tz) => tz,
            _ => TimeZoneInfo.Utc
        };
    }
}
