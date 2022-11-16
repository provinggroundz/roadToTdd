using MyApp.Model;

namespace MyApp.Logic.PersonDateOfBirthProvider;

public interface PersonDateOfBirthProvider
{
    Task<DateTime> GetPersonDateOfBirthFromDatabaseOverTheInternetzzAsync(Person person);
}