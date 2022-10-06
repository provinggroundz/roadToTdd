namespace MyApp.Logic.Helpers
{
    public static class LogicHelpers
    {
        public static Task<DateTime> GetPersonDateOfBirthFromDatabaseOverTheInternetzzAsync(Person person) => Task.FromResult(person.DateOfBirth);
    }
}