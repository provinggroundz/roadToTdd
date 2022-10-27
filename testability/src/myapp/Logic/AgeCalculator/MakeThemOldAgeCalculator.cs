namespace MyApp.Logic.AgeCalculator;

public class MakeThemOldAgeCalculator : AgeCalculator
{
    public Task<int> GetYearsFromDatesAsync(DateTime first, DateTime second)
    {
        return Task.FromResult(100);
    }
}