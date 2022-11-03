using Microsoft.Extensions.Logging;

using MyApp.Model;

namespace MyApp.Logic.PersonMessageProvider;

public class PersonAgeMessageProvider: PersonMessageProvider
{
    private readonly AgeCalculator.AgeCalculator _ageCalculator;
    private readonly PersonDateOfBirthProvider.PersonDateOfBirthProvider _dateOfBirthProvider;
    private readonly DateTimeProvider.DateTimeProvider _dateTimeProvider;
    private readonly ILogger<PersonAgeMessageProvider> _logger;

    public PersonAgeMessageProvider(
        AgeCalculator.AgeCalculator ageCalculator,
        PersonDateOfBirthProvider.PersonDateOfBirthProvider dateOfBirthProvider,
        DateTimeProvider.DateTimeProvider dateTimeProvider,
        ILogger<PersonAgeMessageProvider> logger)
    {
        _ageCalculator = ageCalculator;
        _dateOfBirthProvider = dateOfBirthProvider;
        _dateTimeProvider = dateTimeProvider;
        _logger = logger;
    }
    public async Task<string> ComposeMessageForPerson(Person person)
    {
        _logger.LogInformation("Starting Composing the message");

        var birthDay = await _dateOfBirthProvider.GetPersonDateOfBirthFromDatabaseOverTheInternetzzAsync(person);
        var age = await _ageCalculator.GetYearsFromDatesAsync(birthDay, _dateTimeProvider.Now);

        string message = GenerateMessage(age, person.Name);
        _logger.LogInformation("The message is composed: {Message}", message);
        return message;
    }

    internal string GenerateMessage(int age, string name)
    {
        return age < 18 ? "you shall not pass" : $"{name} is {age} years old";
    }
}
