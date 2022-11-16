//using Microsoft.Extensions.Logging;

using MyApp.Logic.PersonPrinter;
using MyApp.Model;

namespace MyApp.Logic.PersonMessageProvider;

public class DecoratedPersonAgeMessageProvider: PersonMessageProvider, Decorator
{
    private readonly AgeCalculator.AgeCalculator _ageCalculator;
    private readonly PersonDateOfBirthProvider.PersonDateOfBirthProvider _dateOfBirthProvider;
    private readonly DateTimeProvider.DateTimeProvider _dateTimeProvider;
    //private readonly ILogger<DecoratedPersonAgeMessageProvider> _logger;
    private string _decorator;

    public DecoratedPersonAgeMessageProvider(AgeCalculator.AgeCalculator ageCalculator,
        PersonDateOfBirthProvider.PersonDateOfBirthProvider dateOfBirthProvider,
        DateTimeProvider.DateTimeProvider dateTimeProvider)
        //ILogger<DecoratedPersonAgeMessageProvider> logger)
    {
        _ageCalculator = ageCalculator;
        _dateOfBirthProvider = dateOfBirthProvider;
        _dateTimeProvider = dateTimeProvider;
        //_logger = logger;
        _decorator = "********";
    }

    public void SetupDecorator(string newDecorator)
    {
        //_logger.LogInformation("changing decorator from {Old} to {New}", _decorator, newDecorator);
        _decorator = newDecorator;
    }
    
    public async Task<string> ComposeMessageForPerson(Person person)
    {
        //_logger.LogInformation("Composing Message for person {@Person}", person);
        var birthDay = await _dateOfBirthProvider.GetPersonDateOfBirthFromDatabaseOverTheInternetzzAsync(person);
        var age = await _ageCalculator.GetYearsFromDatesAsync(birthDay, _dateTimeProvider.Now);
        var message = $"{_decorator} {person.Name} is {age} years old {_decorator}";
        //_logger.LogDebug("Message Composed: {Message}", message);
        return message;
    }
}