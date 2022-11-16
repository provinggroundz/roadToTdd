using MyApp.Logic.AgeCalculator;

namespace MyAppTests.Logic.AgeCalculator;

public class DefaultAgeCalculatorTests
{
    private MyApp.Logic.AgeCalculator.AgeCalculator _sut;

    public DefaultAgeCalculatorTests()
    {
        _sut = new DefaultAgeCalculator();
    }

    [Fact]
    public async Task GetYearsFromDatesWith2YearsApart_GetsResult2()
    {
        DateTime first = new (2000, 1, 1);
        DateTime second = new(2001, 1, 1);
        var result = await _sut.GetYearsFromDatesAsync(first, second);
        result.Should().Be(1);
    }
    
    [Fact]
    public async Task GetYearsFromDatesWith2YearsApart_GetsResult2_NoMatterWhatOrder()
    {
        DateTime first = new (2000, 1, 1);
        DateTime second = new(2001, 1, 1);
        var result1 = await _sut.GetYearsFromDatesAsync(first, second);
        var result2 = await _sut.GetYearsFromDatesAsync(second, first);
        result1.Should().Be(result2);
    }
}