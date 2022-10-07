namespace MyApp.Logic;

public interface IPersonDateOfBirthProvider
{
    Task<DateTime> GetPersonDateOfBirthFromDatabaseOverTheInternetzzAsync(Person person);
}