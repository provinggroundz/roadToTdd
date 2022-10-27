namespace MyApp.Logic.AgeCalculator
{
    public class DefaultAgeCalculator: AgeCalculator
{
        public Task<int> GetYearsFromDatesAsync(DateTime first, DateTime second)
        {
            return Task.FromResult(second.Month < first.Month ||
                (second.Month == first.Month &&
                second.Day < first.Day)
                ? second.Year - first.Year - 1
                : second.Year - first.Year);
        }
    }
}
