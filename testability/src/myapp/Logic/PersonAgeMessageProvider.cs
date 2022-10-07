namespace MyApp.Logic;

public class PersonAgeMessageProvider: IPersonAgeMessageProvider
{
    private readonly IAgeCalculator _ageCalculator;
    private readonly IPersonDateOfBirthProvider _dateOfBirthProvider;
    private readonly IDateTimeProvider _dateTimeProvider;

    public PersonAgeMessageProvider(
        IAgeCalculator ageCalculator,
        IPersonDateOfBirthProvider dateOfBirthProvider,
        IDateTimeProvider dateTimeProvider)
    {
        _ageCalculator = ageCalculator;
        _dateOfBirthProvider = dateOfBirthProvider;
        _dateTimeProvider = dateTimeProvider;
    }
    public async Task<string> ComposeBirthdayMessageForPerson(Person person)
    {
        var birthDay = await _dateOfBirthProvider.GetPersonDateOfBirthFromDatabaseOverTheInternetzzAsync(person);
        var age = await _ageCalculator.GetYearsFromDatesAsync(birthDay, _dateTimeProvider.Now);
        return $"{person.Name} is {age} years old";
    }
}
