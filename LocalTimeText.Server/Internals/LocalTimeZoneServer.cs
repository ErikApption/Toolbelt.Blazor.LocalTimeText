using Microsoft.AspNetCore.Components.Endpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Toolbelt.Blazor.Globalization.Internals;

/// <summary>
/// Implementation of ILocalTimeZone for Blazor Server runtime.
/// </summary>
internal class LocalTimeZoneServer : LocalTimeZone
{
    private readonly HttpContext? _httpContext;

    private readonly GetLocalTimeZoneAsync _BaseGetLocalTimeZoneAsync;

    private readonly SetLocalTimeZoneAsync _BaseSetLocalTimeZoneAsync;

    /// <summary>
    /// Initializes a new instance of the <see cref="LocalTimeZoneServer"/> class.
    /// </summary>
    /// <param name="services">The service provider.</param>
    public LocalTimeZoneServer(IServiceProvider services) : base(services)
    {
        var componentPrerenderer = services.GetRequiredService<IComponentPrerenderer>();
        this._httpContext = componentPrerenderer
            .GetType()
            .GetProperty("HttpContext", BindingFlags.NonPublic | BindingFlags.Instance)?
            .GetValue(componentPrerenderer) as HttpContext;

        // Override the GetLocalTimeZoneAsync method to implement server-side logic
        this._BaseGetLocalTimeZoneAsync = this.GetLocalTimeZoneAsync;
        this._BaseSetLocalTimeZoneAsync = this.SetLocalTimeZoneAsync;
        this.GetLocalTimeZoneAsync = this._GetLocalTimeZoneServerAsync;
        this.SetLocalTimeZoneAsync = this._SetLocalTimeZoneServerAsync;
    }

    private async ValueTask<TimeZoneInfo> _GetLocalTimeZoneServerAsync(string? fallbackTimeZoneId)
    {
        if (this._timeZone is not null) return this._timeZone;

        if (this._httpContext is not null)
        {
            var timeZoneId = this._httpContext.Request.Cookies.TryGetValue(this.CookieName, out var tzName) ? tzName : fallbackTimeZoneId;
            this._timeZone = this.TimeZoneIdMapper.TimeZoneIdToTimeZoneInfo(timeZoneId) ?? TimeZoneInfo.Utc;
            return this._timeZone;
        }

        return await this._BaseGetLocalTimeZoneAsync(fallbackTimeZoneId);
    }

    private async ValueTask _SetLocalTimeZoneServerAsync(string timeZoneId)
    {
        if (this._timeZone?.Id == timeZoneId) return;

        if (this._httpContext is not null)
        {
            this._timeZone = this.TimeZoneIdMapper.TimeZoneIdToTimeZoneInfo(timeZoneId);
            this._httpContext.Response.Cookies.Append(this.CookieName, this._timeZone.Id, new CookieOptions
            {
                Expires = this.UseSessionOnlyCookie ? null : DateTimeOffset.MaxValue
            });

            await this.FireLocalTimeZoneChanged(this._timeZone);
        }
        else
        {
            await this._BaseSetLocalTimeZoneAsync(timeZoneId);
        }
    }
}
