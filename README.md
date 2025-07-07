# Blazor Local Time Text

[![NuGet](https://img.shields.io/nuget/v/Toolbelt.Blazor.LocalTimeText.svg)](https://www.nuget.org/packages/Toolbelt.Blazor.LocalTimeText/) [![unit tests](https://github.com/jsakamoto/Toolbelt.Blazor.LocalTimeText/actions/workflows/unit-tests.yml/badge.svg)](https://github.com/jsakamoto/Toolbelt.Blazor.LocalTimeText/actions/workflows/unit-tests.yml) [![Discord](https://img.shields.io/discord/798312431893348414?style=flat&logo=discord&logoColor=white&label=Blazor%20Community&labelColor=5865f2&color=gray)](https://discord.com/channels/798312431893348414/1202165955900473375)

A Blazor component that displays a local time text in the user's local time zone.

![](https://raw.githubusercontent.com/jsakamoto/Toolbelt.Blazor.LocalTimeText/refs/heads/main/.assets/social-media.png)

🚀 Live Demo is here: [https://demo-blazor-localtimetext-static.azurewebsites.net/](https://demo-blazor-localtimetext-static.azurewebsites.net/)

This component supports all of the Blazor hosting models, including Blazor WebAssembly, Blazor Server, Blazor SSR, and Auto render mode. The live demo site above is running on the Blazor Server-side Static render mode.

## 📦 Installation

### For Blazor WebAssembly projects

You can install the package by executing the following command:
dotnet add package Toolbelt.Blazor.LocalTimeText
You also need to register the service for the Blazor WebAssembly platform in the `Program.cs`.
using Toolbelt.Blazor.Extensions.DependencyInjection;
...
builder.Services.AddLocalTimeZoneWebAssembly();
### For Blazor Server projects

You should install the server version package by executing the following command:
dotnet add package Toolbelt.Blazor.LocalTimeText.Server
You also need to register the service for the Blazor Server platform in the `Program.cs`.
using Toolbelt.Blazor.Extensions.DependencyInjection;
...
builder.Services.AddLocalTimeZoneServer();
I recommend adding the following line to the `<head>` section of your `Pages/_Host.cshtml` or `Components/App.razor` file to initialize the user's local time zone. Otherwise, the first time the user accesses the page, the time zone will be displayed in the UTC time zone.
<html lang="en">
<head>
    <meta charset="utf-8" />
    ...
    <!-- 👇 Add this line -->
    <script src="_content/Toolbelt.Blazor.LocalTimeText/initialize-local-timezone.min.js"></script>
    ...
## 🚀 Usage

Add the following line to your `_Imports.razor` file:
@using Toolbelt.Blazor.Globalization
Then you can use the `LocalTimeText` component like this:
<LocalTimeText Time="9:00 AM" TimeZone="PST" />
The above markup means the time `9:00 AM` is in the `PST` time zone.

As a result, the component will display the time in the user's local time zone. For example, if the user is in the `America/New_York` time zone, the component will display `12:00 PM`.
12:00 PM
The `LocalDateTime` component is similar, but takes a `DateTimeOffset` (or `DateTime`) object instead of separate `Date` and `Time` strings.
<LocalDateTime DateTime="someDateTimeOffset" />
### Parameters

#### `LocalTimeText` Parameters

Name       | Type     | Description
-----------|----------| -----------
`Date`     | `string` | The date to be displayed. The format is `yyyy-MM-dd` (e.g., `2022-01-01`). This parameter is optional. If not provided, the current date will be used. However, when the specified time zone has daylight saving time changes, this parameter is essential to the correct time conversion.
`Time`     | `string` | The time to be displayed in the time zone specified by the `TimeZone` parameter. The format is `H:mm`, `HH:mm`, `H:mm tt`, or `HH:mm tt` (e.g., `9:00 AM`).
`TimeZone` | `string` | The time zone of the `Time` parameter. The value is a time zone ID (e.g., `PST`, `Pacific Standard Time`, `America/Los_Angeles`). When this parameter is not provided, the UTC zone will be used.
`Format`   | `string` | The format to display the time text. By default, it will infer the format from the `Time` parameter.

#### `LocalDateTime` Parameters

Name             | Type             | Description
-----------------|------------------|------------
`DateTime`       | `DateTimeOffset` | The date and time to be displayed.
`TimeZone`       | `string`         | Optional override of the user's local time zone.
`SourceTimeZone` | `string`         | Override for the time zone of the `DateTime` parameter. Useful when dealing with UTC `DateTime` values.
`Format`         | `string`         | The format to display the time text. By default, it will use the general date/time pattern (short time) "g".
`LowerCaseAmPm`  | `bool`           | If `true`, lowercases the AM/PM designator.

### Customizing the display text

#### Formatting

You can customize the output text formatting by specifying the `Format` parameter. For example, if you want to show the date text only in the `<LocalTimeText>` component, you can use the following code:
<LocalTimeText Date="2025/02/24" Time="9:00 AM" TimeZone="PST" Format="yyyy-MM-dd" />
The above markup will show users the text like below.
2025-02-24

#### Templating

The `LocalTimeText` component allows you to customize not only formatting but also the entire rendering text. You can do that by using the `ChildContent` render fragment parameter. The information about what should be displayed is provided by the `LocalTimeTextTemplateContext` object via the `context` argument.

For example, the following code,
<LocalTimeText Time="9:00 AM" TimeZone="PST">
  <div style="display: inline-block;">
    <div class="time-text">
      @context.Value.ToString("HH:mm tt").ToLower()
    </div>
    <div class="time-zone-text">
      (@context.LocalTimeZone.DisplayName)
    </div>
  </div>
</LocalTimeText>
it will display the time and time zone in the user's local time zone, like this:
12:00 pm
(Eastern Standard Time)
### Local Time Zone Selector component

This package also provides a component, `LocalTimeZoneSelector`, that allows the user to select the local time zone. You can use the component like this:
<LocalTimeZoneSelector />
The `LocalTimeZoneSelector` component will display a dropdown list of time zones. When the user selects a time zone, the user's local time zone and all of the display of the `<LocalTimeText>` component on the app will be changed. By default, the result of the time zone selection is persisted in the cookie with the name "tz".

## 💡 How does it work?

The `LocalTimeText` component uses the `ILocalTimeZone` service to get the user's local time zone. The service is registered in the DI container when you call the `AddLocalTimeZoneWebAssembly` or `AddLocalTimeZoneServer` method.

The `ILocalTimeZone` service refers to a cookie value named "tz" to get or set the user's local time zone. The service also provides the event that fires when the local time zone is changed.

The JavaScript code in the `initialize-local-timezone.min.js` reloads the page after setting the "tz" cookie to be the user's local time zone, which comes from the `Intl.DateTimeFormat().resolvedOptions().timeZone` browser API, when the "tz" cookie is not set.

Using a cookie to determine the local time zone is a common practice in server-side rendering because there is no way to know the user's local time zone when the page is initially rendering.

Even in interactive render mode, such as Blazor WebAssembly mode, the `ILocalTimeZone` service gets the time zone by referring to the `Intl.DateTimeFormat().resolvedOptions().timeZone` value only when the "tz" cookie is not set. This is an intentional design to keep the behavior consistent with any other render mode, like "Auto" render mode.

## ⚙️ Configuration

You can configure the default behavior of the `ILocalTimeZone` service when you register the service in the `Program.cs` file. Specifically, you can specify the callback function to the registration method to rewrite the member of the `ILocalTimeZone` service in that callback function. The following list is the members of the `ILocalTimeZone` service.

Name                    | Type                | Writable | Description
------------------------|---------------------|----------|-------------
`Services`              | `IServiceProvider`  |     | The `IServiceProvider` instance which can be used to resolve the dependencies.
`TimeZoneIdMapper`      | `ITimeZoneIdMapper` |     | The `ITimeZoneIdMapper` instance that can be used to map the time zone ID family, like `PST`, to the IANA time zone ID, like `America/Los_Angeles`.
`CookieName`            | `string`            | Yes | Gets or sets the cookie's name that stores the user's local time zone. The default value is "tz".
`UseSessionOnlyCookie`  | `bool`              | Yes | If `true`, the cookie will be session-only. The default value is `false`, meaning the value will be persistent.
`LocalTimeZoneChanged`  | event               |     | This is the event that fires when the local time zone changes. The event handler must be an async method returning `Task` that takes one argument, the `LocalTimeZoneEventArgs` object.
`GetLocalTimeZoneAsync` | delegate            | Yes | This delegate returns the user's local time zone. The signature of this delegate is `ValueTask<TimeZoneInfo> GetLocalTimeZoneAsync(string? fallbackTimeZoneId)`. The default implementation is to get the time zone from the cookie. If the cookie is not set, it will return the time zone from the `Intl.DateTimeFormat().resolvedOptions().timeZone` browser API if the rendering process is in any interactive render mode. Otherwise, it will return the argument, `fallbackTimeZoneId`. You can replace this delegate to entirely replace the behavior of getting the local time zone, like getting the time zone from the user's profile or any other source.
`SetLocalTimeZoneAsync` | delegate            | Yes | This delegate sets the user's local time zone. The signature of this delegate is `ValueTask SetLocalTimeZoneAsync(string timeZoneId)`. The default implementation is to set the time zone specified in the argument to the cookie. The name of the cookie comes from the `CookieName`. If the `UseSessionOnlyCookie` is `true`, the cookie will be session-only. The default implementation also fires the `LocalTimeZoneChanged` event. You can replace this delegate to entirely replace the behavior of setting the local time zone, like saving the time zone to the user's profile or any other source.

### Notice: for the Blazor Server case

When you change the name of the cookie that is to save the user's local time zone in the callback function passed to the service registration method, you should also change the `<script>` tag in the `<head>` section of your `Pages/_Host.cshtml` or `Components/App.razor` file to be added the `cookie-name` attribute to specify the same cookie name with the app runs at a server, like below.
<html lang="en">
<head>
    <meta charset="utf-8" />
    ...
    <script
        src="_content/Toolbelt.Blazor.LocalTimeText/initialize-local-timezone.min.js"
        cookie-name="time-zone"></script>
        <!-- 👆 Add this attribute -->
    ...


## 📖 Release Note

[Release notes](https://github.com/jsakamoto/Toolbelt.Blazor.LocalTimeText/blob/main/RELEASE-NOTES.txt)

## ✉️ License

[Mozilla Public License Version 2.0](https://github.com/jsakamoto/Toolbelt.Blazor.LocalTimeText/blob/main/LICENSE)
