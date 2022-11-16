using Microsoft.Extensions.Logging;

using MyApp.Logic.PersonMessageProvider;
using MyApp.Model;

namespace MyAppTests.Logic.PersonMessageProvider;

public class PersonAgeMessageProviderTests
{
    private readonly MyApp.Logic.PersonMessageProvider.PersonMessageProvider _sut;
    private readonly MyApp.Logic.AgeCalculator.AgeCalculator _ageCalculator;
    private readonly MyApp.Logic.PersonDateOfBirthProvider.PersonDateOfBirthProvider _dateOfBirthProvider;
    private readonly MyApp.Logic.DateTimeProvider.DateTimeProvider _dateTimeProvider;
    private readonly ILogger<PersonAgeMessageProvider> _logger;

    public PersonAgeMessageProviderTests()
    {
        _ageCalculator = A.Fake<MyApp.Logic.AgeCalculator.AgeCalculator>();
        _dateOfBirthProvider = A.Fake<MyApp.Logic.PersonDateOfBirthProvider.PersonDateOfBirthProvider>();
        _dateTimeProvider = A.Fake<MyApp.Logic.DateTimeProvider.DateTimeProvider>();
        _logger = A.Fake<ILogger<PersonAgeMessageProvider>>();
        _sut = new PersonAgeMessageProvider(_ageCalculator, _dateOfBirthProvider, _dateTimeProvider, _logger);
    }

    [Fact]
    public async Task ComposeMessageForPerson_GetAgeOfPerson()
    {
        Person maxi = new("Maxi", 4) { DateOfBirth = new DateTime(1981, 11, 3) };
        Person callArgument = default(Person)!;
        A.CallTo(() => _dateOfBirthProvider.GetPersonDateOfBirthFromDatabaseOverTheInternetzzAsync(A<Person>.Ignored))
            .Invokes(call =>
            {
                callArgument = (Person)call.Arguments[0]!;
            })
            .Returns(maxi.DateOfBirth);
        DateTime now = DateTime.Now;
        A.CallTo(() => _dateTimeProvider.Now).Returns(now);

        A.CallTo(() =>
                _ageCalculator.GetYearsFromDatesAsync(A<DateTime>.Ignored, A<DateTime>.That.Matches(dt => dt == now)))
            .Returns(41);

        var result = await _sut.ComposeMessageForPerson(maxi);

        result.Should().Be($"{maxi.Name} is 41 years old");
        callArgument.Should().Be(maxi);
    }
}