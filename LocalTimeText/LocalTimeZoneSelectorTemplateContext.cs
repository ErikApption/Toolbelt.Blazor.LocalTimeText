namespace Toolbelt.Blazor.Globalization;

/// <summary>
/// Represents the template context for the local time zone selector items.
/// </summary>
public class LocalTimeZoneSelectorTemplateContext
{
    /// <summary>
    /// Gets the time zone information of a time zone selection item.
    /// </summary>
    public required TimeZoneInfo TimeZoneInfo { get; init; }
}
