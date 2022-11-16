using MyApp.Logic.AgeCalculator;

namespace MyAppTests.Logic.AgeCalculator;

public class MakeThemOldAgeCalculatorTests
{
    private readonly MyApp.Logic.AgeCalculator.AgeCalculator _sut;

    public MakeThemOldAgeCalculatorTests()
    {
        _sut = new MakeThemOldAgeCalculator();
    }

    [Fact]
    public async Task GetYearsFromDatesAsync_AlwaysReturns100()
    {
        int expected = 100;
        (await _sut.GetYearsFromDatesAsync(DateTime.Now, DateTime.Now)).Should().Be(expected);
        (await _sut.GetYearsFromDatesAsync(DateTime.Today.AddYears(-1), DateTime.Now)).Should().Be(expected);
        (await _sut.GetYearsFromDatesAsync(DateTime.Now.AddYears(-2), DateTime.Now.AddYears(-5))).Should().Be(expected);
    }
}