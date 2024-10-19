namespace Toolbelt.Blazor.Globalization;

/// <summary>
/// Represents the context for a local time text template.
/// </summary>
public class LocalTimeTextTemplateContext
{
    /// <summary>
    /// Gets the date and time value in the local time zone.
    /// </summary>
    public required DateTimeOffset Value { get; init; }

    /// <summary>
    /// Gets the local time zone information.
    /// </summary>
    public required TimeZoneInfo LocalTimeZone { get; init; }

    /// <summary>
    /// Gets the format string for the date and time value.
    /// </summary>
    public required string Format { get; init; }

    /// <summary>
    /// Gets the converter whether the time text should be lower case or upper case.
    /// </summary>
    public required Func<string, string> Capitalize { get; init; }
}
