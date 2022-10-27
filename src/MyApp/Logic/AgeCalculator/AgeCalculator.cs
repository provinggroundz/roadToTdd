namespace MyApp.Logic.AgeCalculator
{
    public interface AgeCalculator
    {
        Task<int> GetYearsFromDatesAsync(DateTime first, DateTime second);
    }
}