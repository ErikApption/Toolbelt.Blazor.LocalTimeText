namespace Toolbelt.Blazor.Globalization;

/// <summary>
/// Interface representing the local time zone functionality.
/// </summary>
public interface ILocalTimeZone
{
    /// <summary>
    /// Gets the service provider.
    /// </summary>
    IServiceProvider Services { get; }

    /// <summary>
    /// Gets the time zone ID mapper service.
    /// </summary>
    ITimeZoneIdMapper TimeZoneIdMapper { get; }

    /// <summary>
    /// Gets or sets the name of the cookie used to store the local time zone.
    /// </summary>
    string CookieName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to use a session-only cookie.
    /// </summary>
    bool UseSessionOnlyCookie { get; set; }

    /// <summary>
    /// Event triggered when the local time zone changes.
    /// </summary>
    event Func<LocalTimeZoneEventArgs, Task>? LocalTimeZoneChanged;

    /// <summary>
    /// Gets or sets the asynchronous function to get the local time zone.
    /// </summary>
    GetLocalTimeZoneAsync GetLocalTimeZoneAsync { get; set; }

    /// <summary>
    /// Gets or sets the asynchronous function to set the local time zone.
    /// </summary>
    SetLocalTimeZoneAsync SetLocalTimeZoneAsync { get; set; }
}
