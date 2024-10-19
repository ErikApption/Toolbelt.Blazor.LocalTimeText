using Bunit;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace Toolbelt.Blazor.Globalization.Test;

public class LocalTimeTextTest
{
    [Test]
    public void InferFormat_Test()
    {
        LocalTimeText.InferFormat("9:00").Format.Is("H:mm");
        LocalTimeText.InferFormat("21:00").Format.Is("H:mm");
        LocalTimeText.InferFormat("09:00").Format.Is("HH:mm");
        LocalTimeText.InferFormat("10:00AM").Format.Is("h:mmtt");
        LocalTimeText.InferFormat("10:00 pm").Format.Is("h:mm tt");

        LocalTimeText.InferFormat("2024/1/23 9:00").Format.Is("H:mm");
        LocalTimeText.InferFormat("2024/01/23 21:00").Format.Is("H:mm");
        LocalTimeText.InferFormat("1/23/2024 09:00").Format.Is("HH:mm");
        LocalTimeText.InferFormat("01/23/2024 10:00am").Format.Is("h:mmtt");
        LocalTimeText.InferFormat("2024/1/23 10:00 PM").Format.Is("h:mm tt");
    }

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

        var cut = testContext.RenderComponent<LocalTimeText>(buulder =>
        {
            buulder.Add(_ => _.Date, "2024/11/03");
            buulder.Add(_ => _.Time, "12:30 am");
            buulder.Add(_ => _.TimeZone, "PST");
        });
    
        cut.Markup.Is("4:30 pm");
    }

    [Test]
    public void NoSummerTime_to_StandardTime_Test()
    {
        using var testContext = CreateTestContext(localTimeZone: "Asia/Tokyo");

        var cut = testContext.RenderComponent<LocalTimeText>(buulder =>
        {
            buulder.Add(_ => _.Date, "2024/11/04");
            buulder.Add(_ => _.Time, "12:30AM");
            buulder.Add(_ => _.TimeZone, "PST");
        });

        cut.Markup.Is("5:30PM");
    }

    [Test]
    public void DaylightTime_to_NoSummerTime_Test()
    {
        using var testContext = CreateTestContext(localTimeZone: "America/Los_Angeles");

        var cut = testContext.RenderComponent<LocalTimeText>(buulder =>
        {
            buulder.Add(_ => _.Date, "2024/11/03");
            buulder.Add(_ => _.Time, "04:30 AM");
            buulder.Add(_ => _.TimeZone, "JST");
        });

        cut.Markup.Is("12:30 PM");
    }

    [Test]
    public void StandardTime_to_NoSummerTime_Test()
    {
        using var testContext = CreateTestContext(localTimeZone: "America/Los_Angeles");

        var cut = testContext.RenderComponent<LocalTimeText>(buulder =>
        {
            buulder.Add(_ => _.Date, "2024/11/04");
            buulder.Add(_ => _.Time, "04:30am");
            buulder.Add(_ => _.TimeZone, "JST");
        });

        cut.Markup.Is("11:30am");
    }

}