using MyApp.Logic.DateTimeProvider;

namespace MyAppTests.Logic.DateTimeProvider;

public class CurrentDateTimeProviderTests
{
    private readonly MyApp.Logic.DateTimeProvider.DateTimeProvider _sut;

    public CurrentDateTimeProviderTests()
    {
        _sut = new CurrentDateTimeProvider();
    }

    [Fact]
    public void CurrentDateTimeProvider_GetsCurrentTime()
    {
        DateTime expected = DateTime.Now;
        var now = _sut.Now;
        now.Should().BeCloseTo(expected, TimeSpan.FromMilliseconds(50));
    }
}