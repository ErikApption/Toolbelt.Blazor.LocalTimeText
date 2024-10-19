namespace Toolbelt.Blazor.Globalization;

/// <summary>
/// Delegate for asynchronously retrieving the local time zone information.
/// </summary>
/// <param name="fallbackTimeZoneId">The fallback time zone ID to use if the local time zone cannot be determined.</param>
/// <returns>A task that represents the asynchronous operation. The task result contains the local <see cref="TimeZoneInfo"/>.</returns>
public delegate ValueTask<TimeZoneInfo> GetLocalTimeZoneAsync(string? fallbackTimeZoneId);

/// <summary>
/// Delegate for asynchronously setting the local time zone.
/// </summary>
/// <param name="timeZoneId">The time zone ID to set as the local time zone.</param>
/// <returns>A task that represents the asynchronous operation.</returns>
public delegate ValueTask SetLocalTimeZoneAsync(string timeZoneId);
