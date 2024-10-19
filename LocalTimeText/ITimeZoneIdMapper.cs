namespace Toolbelt.Blazor.Globalization;

/// <summary>
/// Provides methods to map Windows time zone IDs and time zone abbreviations to IANA IDs, 
/// and to convert time zone IDs to TimeZoneInfo objects.
/// </summary>
public interface ITimeZoneIdMapper
{
    /// <summary>
    /// Tries to convert the given Windows time zone ID to its corresponding IANA ID.
    /// </summary>
    /// <param name="winTzId">The Windows time zone ID to convert, like "Pacific Standard Time".</param>
    /// <param name="ianaId">The corresponding IANA ID if the conversion is successful, like "America/Los_Angeles"; otherwise, an empty string.</param>
    /// <returns><c>true</c> if the conversion is successful; otherwise, <c>false</c>.</returns>
    bool TryConvertWinTzToIanaId(string winTzId, out string ianaId);

    /// <summary>
    /// Tries to convert the given time zone abbreviation to its corresponding IANA ID.
    /// </summary>
    /// <param name="tzAbbreviation">The time zone abbreviation to convert, like "PST".</param>
    /// <param name="ianaId">The corresponding IANA ID if the conversion is successful, like "America/Los_Angeles"; otherwise, an empty string.</param>
    /// <returns><c>true</c> if the conversion is successful; otherwise, <c>false</c>.</returns>
    bool TryConvertTzAbbreviationToIanaId(string tzAbbreviation, out string ianaId);

    /// <summary>
    /// Converts the given time zone ID to a corresponding TimeZoneInfo object.
    /// </summary>
    /// <param name="timeZoneId">The time zone ID to convert.</param>
    /// <returns>The corresponding TimeZoneInfo object.</returns>
    TimeZoneInfo TimeZoneIdToTimeZoneInfo(string? timeZoneId);
}
