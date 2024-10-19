using Toolbelt.Blazor.Globalization.Internals;

namespace Toolbelt.Blazor.LocalTimeText.Test;

public class TzIdConverterTest
{
    [Test]
    public void TryConvertWinTzToIanaId_Found_Test()
    {
        var mapper = new TimeZoneIdMapper();
        mapper.TryConvertWinTzToIanaId("Tokyo Standard Time", out var ianaId).IsTrue();
        ianaId.Is("Asia/Tokyo");
    }
    
    [Test]
    public void TryConvertWinTzToIanaId_NotFound_Test()
    {
        var mapper = new TimeZoneIdMapper();
        mapper.TryConvertWinTzToIanaId("JST", out var ianaId).IsFalse();
    }

    [Test]
    public void TryConvertTzAbbreviationToIanaId_Found_Test()
    {
        var mapper = new TimeZoneIdMapper();
        mapper.TryConvertTzAbbreviationToIanaId("JST", out var ianaId).IsTrue();
        ianaId.Is("Asia/Tokyo");
    }

    [Test]
    public void TryConvertTzAbbreviationToIanaId_NotFound_Test()
    {
        var mapper = new TimeZoneIdMapper();
        mapper.TryConvertTzAbbreviationToIanaId("Tokyo Standard Time", out var ianaId).IsFalse();
    }
}