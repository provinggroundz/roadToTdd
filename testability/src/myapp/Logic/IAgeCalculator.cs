namespace MyApp.Logic
{
    public interface IAgeCalculator
    {
        Task<int> GetYearsFromDatesAsync(DateTime first, DateTime second);
    }
}