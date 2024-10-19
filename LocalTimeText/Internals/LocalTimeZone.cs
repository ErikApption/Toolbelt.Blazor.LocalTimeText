using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace Toolbelt.Blazor.Globalization.Internals;

/// <summary>
/// Implementation of ILocalTimeZone for Blazor WebAssembly runtime.
/// </summary>
internal class LocalTimeZone : ILocalTimeZone, IAsyncDisposable
{
    protected TimeZoneInfo? _timeZone = null;

    private readonly IJSRuntime _jSRuntime;

    private IJSObjectReference? _jsModule;

    /// <summary>
    /// Gets the service provider.
    /// </summary>
    public IServiceProvider Services { get; }

    /// <summary>
    /// Gets the time zone ID mapper service.
    /// </summary>
    public ITimeZoneIdMapper TimeZoneIdMapper { get; }

    /// <summary>
    /// Gets or sets the name of the cookie used to store the local time zone.
    /// </summary>
    public string CookieName { get; set; } = "tz";

    /// <summary>
    /// Gets or sets a value indicating whether to use a session-only cookie.
    /// </summary>
    public bool UseSessionOnlyCookie { get; set; }

    /// <summary>
    /// Event triggered when the local time zone changes.
    /// </summary>
    public event Func<LocalTimeZoneEventArgs, Task>? LocalTimeZoneChanged;

    /// <summary>
    /// Gets or sets the asynchronous function to get the local time zone.
    /// </summary>
    public GetLocalTimeZoneAsync GetLocalTimeZoneAsync { get; set; }

    /// <summary>
    /// Gets or sets the asynchronous function to set the local time zone.
    /// </summary>
    public SetLocalTimeZoneAsync SetLocalTimeZoneAsync { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LocalTimeZone"/> class.
    /// </summary>
    /// <param name="services">The service provider.</param>
    public LocalTimeZone(IServiceProvider services)
    {
        this.Services = services;
        this.TimeZoneIdMapper = services.GetRequiredService<ITimeZoneIdMapper>();
        this.GetLocalTimeZoneAsync = this._GetLocalTimeZoneAsync;
        this.SetLocalTimeZoneAsync = this._SetLocalTimeZoneAsync;
        this._jSRuntime = services.GetRequiredService<IJSRuntime>();
    }

    private async ValueTask<IJSObjectReference> GetModuleAsync()
    {
        this._jsModule ??= await this._jSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Toolbelt.Blazor.LocalTimeText/script.min.js");
        return this._jsModule;
    }

    private async ValueTask<TimeZoneInfo> _GetLocalTimeZoneAsync(string? fallbackTimeZoneId)
    {
        if (this._timeZone is not null) return this._timeZone;

        if (OperatingSystem.IsBrowser())
        {
            this._timeZone = this.TimeZoneIdMapper.TimeZoneIdToTimeZoneInfo(TimeZoneInfo.Local.Id);

            var getLocalTimeZoneAwaiter = this._GetLocalTimeZoneAsync().ConfigureAwait(false).GetAwaiter();
            getLocalTimeZoneAwaiter.OnCompleted(() =>
            {
                var timeZoneInfo = getLocalTimeZoneAwaiter.GetResult();
                if (this._timeZone.Id != timeZoneInfo.Id)
                {
                    this._timeZone = timeZoneInfo;
                    this.FireLocalTimeZoneChanged(this._timeZone).ConfigureAwait(false);
                }
            });

            return this._timeZone;
        }
        else
        {
            this._timeZone = await this._GetLocalTimeZoneAsync();
            return this._timeZone;
        }
    }

    private async ValueTask<TimeZoneInfo> _GetLocalTimeZoneAsync()
    {
        var module = await this.GetModuleAsync();
        var localTimeZone = await module.InvokeAsync<string>("getLocalTimeZone", this.CookieName);
        return this.TimeZoneIdMapper.TimeZoneIdToTimeZoneInfo(localTimeZone);
    }

    private async ValueTask _SetLocalTimeZoneAsync(string timeZoneId)
    {
        if (this._timeZone?.Id == timeZoneId) return;

        this._timeZone = this.TimeZoneIdMapper.TimeZoneIdToTimeZoneInfo(timeZoneId);

        var module = await this.GetModuleAsync();
        await module.InvokeVoidAsync("saveLocalTimeZone", this.CookieName, this._timeZone.Id, this.UseSessionOnlyCookie);

        await this.FireLocalTimeZoneChanged(this._timeZone);
    }

    protected async ValueTask FireLocalTimeZoneChanged(TimeZoneInfo timeZone)
    {
        var eventArgs = new LocalTimeZoneEventArgs { TimeZone = timeZone };
        var invocationList = LocalTimeZoneChanged?.GetInvocationList().Cast<Func<LocalTimeZoneEventArgs, Task>>() ?? [];
        await Task.WhenAll(invocationList.Select(invocation => invocation.Invoke(eventArgs)));
    }

    public async ValueTask DisposeAsync()
    {
        if (this._jsModule is not null)
        {
            try { await this._jsModule.DisposeAsync(); }
            catch (JSDisconnectedException) { }
        }
    }
}
