namespace Toolbelt.Blazor.Globalization;

/// <summary>
/// Represents the event arguments for local time zone events in the Toolbelt.Blazor.Globalization namespace.
/// </summary>
public class LocalTimeZoneEventArgs : EventArgs
{
    /// <summary>
    /// Gets the latest time zone information.
    /// </summary>
    public required TimeZoneInfo TimeZone { get; init; }
}
