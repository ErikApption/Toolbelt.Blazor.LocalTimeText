using Bunit;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace Toolbelt.Blazor.Globalization.Test;

public class LocalDateTimeTest
{

    private static Bunit.TestContext CreateTestContext(string localTimeZone)
    {
        var testContext = new Bunit.TestContext();
        testContext.Services.AddLocalTimeZoneWebAssembly();
        var module = testContext.JSInterop.SetupModule("./_content/Toolbelt.Blazor.LocalTimeText/script.min.js");
        module.Setup<string>("getLocalTimeZone", "tz").SetResult(localTimeZone);
        return testContext;
    }

    [Test]
    public void NoSummerTime_to_DaylightTime_Test()
    {
        using var testContext = CreateTestContext(localTimeZone: "Asia/Tokyo");
        var dt = new DateTimeOffset(2024, 11, 3, 0, 30, 0, TimeSpan.Zero);

        var cut = testContext.RenderComponent<LocalDateTime>(buulder =>
        {
            buulder.Add(_ => _.DateTime, dt);
            buulder.Add(_ => _.Format, "h:mm tt");
            buulder.Add(_ => _.LowerCaseAmPm, true);
        });
    
        cut.Markup.Is("9:30 am");
    }

    [Test]
    public void NoSummerTime_to_StandardTime_Test()
    {
        using var testContext = CreateTestContext(localTimeZone: "Asia/Tokyo");
        var dt = new DateTimeOffset(2024, 11, 3, 0, 30, 0, TimeSpan.Zero);

        var cut = testContext.RenderComponent<LocalDateTime>(buulder =>
        {
            buulder.Add(_ => _.DateTime, dt);
            buulder.Add(_ => _.Format, "h:mmtt");
        });

        cut.Markup.Is("9:30AM");
    }

    [Test]
    public void DaylightTime_to_NoSummerTime_Test()
    {
        using var testContext = CreateTestContext(localTimeZone: "America/Los_Angeles");
        var format = "yyyy/MM/dd hh:mm tt zzz";
        var dto = DateTimeOffset.ParseExact("2024/11/03 04:30 AM +09:00", format, System.Globalization.CultureInfo.InvariantCulture);

        var cut = testContext.RenderComponent<LocalDateTime>(buulder =>
        {
            buulder.Add(_ => _.DateTime, dto);
            buulder.Add(_ => _.Format, "h:mm tt");
        });

        cut.Markup.Is("12:30 PM");
    }


}