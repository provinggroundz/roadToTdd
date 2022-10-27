using MyApp.Model;

namespace MyApp.Logic.PersonMessageProvider;

public class PersonAgeMessageProvider: PersonMessageProvider
{
    private readonly AgeCalculator.AgeCalculator _ageCalculator;
    private readonly PersonDateOfBirthProvider.PersonDateOfBirthProvider _dateOfBirthProvider;
    private readonly DateTimeProvider.DateTimeProvider _dateTimeProvider;

    public PersonAgeMessageProvider(
        AgeCalculator.AgeCalculator ageCalculator,
        PersonDateOfBirthProvider.PersonDateOfBirthProvider dateOfBirthProvider,
        DateTimeProvider.DateTimeProvider dateTimeProvider)
    {
        _ageCalculator = ageCalculator;
        _dateOfBirthProvider = dateOfBirthProvider;
        _dateTimeProvider = dateTimeProvider;
    }
    public async Task<string> ComposeMessageForPerson(Person person)
    {
        var birthDay = await _dateOfBirthProvider.GetPersonDateOfBirthFromDatabaseOverTheInternetzzAsync(person);
        var age = await _ageCalculator.GetYearsFromDatesAsync(birthDay, _dateTimeProvider.Now);
        return $"{person.Name} is {age} years old";
    }
}
