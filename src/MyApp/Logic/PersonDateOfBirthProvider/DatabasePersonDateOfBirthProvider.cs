using MyApp.Model;

namespace MyApp.Logic.PersonDateOfBirthProvider
{
    public class DatabasePersonDateOfBirthProvider: PersonDateOfBirthProvider
    {
        public async Task<DateTime> GetPersonDateOfBirthFromDatabaseOverTheInternetzzAsync(Person person)
        {
            await Task.Delay(3000);
            return person.DateOfBirth;
        }
    }
}
