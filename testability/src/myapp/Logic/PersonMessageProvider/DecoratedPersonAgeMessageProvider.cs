using MyApp.Logic.PersonPrinter;
using MyApp.Model;

namespace MyApp.Logic.PersonMessageProvider;

public class DecoratedPersonAgeMessageProvider: PersonMessageProvider, Decorator
{
    private readonly AgeCalculator.AgeCalculator _ageCalculator;
    private readonly PersonDateOfBirthProvider.PersonDateOfBirthProvider _dateOfBirthProvider;
    private readonly DateTimeProvider.DateTimeProvider _dateTimeProvider;
    private string _decorator;

    public DecoratedPersonAgeMessageProvider(AgeCalculator.AgeCalculator ageCalculator,
        PersonDateOfBirthProvider.PersonDateOfBirthProvider dateOfBirthProvider,
        DateTimeProvider.DateTimeProvider dateTimeProvider)
    {
        _ageCalculator = ageCalculator;
        _dateOfBirthProvider = dateOfBirthProvider;
        _dateTimeProvider = dateTimeProvider;
        _decorator = "********";
    }

    public void SetupDecorator(string newDecorator)
    {
        _decorator = newDecorator;
    }
    
    public async Task<string> ComposeMessageForPerson(Person person)
    {
        var birthDay = await _dateOfBirthProvider.GetPersonDateOfBirthFromDatabaseOverTheInternetzzAsync(person);
        var age = await _ageCalculator.GetYearsFromDatesAsync(birthDay, _dateTimeProvider.Now);
        return $"{_decorator}{person.Name} is {age} years old{_decorator}";
    }
}