using MyApp.Logic.Helpers;

namespace MyApp.Logic;

public class PersonAgePrinter
{
    readonly List<Person> _people = new();

    public PersonAgePrinter()
    {
        _people.Add(new Person("Baad Developier", 1) { DateOfBirth = new DateTime(1966,6,6)});
        _people.Add(new Person("Good Developier", 2) { DateOfBirth = new DateTime(1977,7,7)});
        _people.Add(new Person("Metwally Developier", 3) { DateOfBirth = new DateTime(1981,11,3)});
        _people.Add(new Person("Dave Developier", 4) { DateOfBirth = new DateTime(1988,8,8)});
    }

    public async Task PrintByIdAsync(int id)
    {
        var foundPerson = await GetPersonByIdAsync(id);
        var birthDay = await LogicHelpers.GetPersonDateOfBirthFromDatabaseOverTheInternetzzAsync(foundPerson);
        var age = await GetYearsFromDatesAsync(birthDay, DateTime.Now);
        Console.WriteLine($"The person named {foundPerson.Name} is a whopping {age} years old");
    }

    private Task<Person> GetPersonByIdAsync(int id)
    {
        var person = _people.First(p => p.Id == id);
        return Task.FromResult(person);
    }

    private static Task<int> GetYearsFromDatesAsync(DateTime first, DateTime second)
    {
        return Task.FromResult(second.Month < first.Month ||
            (second.Month == first.Month &&
            second.Day < first.Day)
            ? second.Year - first.Year - 1
            : second.Year - first.Year);
    }
}
