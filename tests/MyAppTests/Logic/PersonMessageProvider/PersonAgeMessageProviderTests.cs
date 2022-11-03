using MyApp.Logic.PersonMessageProvider;
using FakeItEasy;
using MyApp.Logic.AgeCalculator;
using MyApp.Logic.DateTimeProvider;
using MyApp.Logic.PersonDateOfBirthProvider;
using Microsoft.Extensions.Logging;
using MyApp.Model;
using FluentAssertions;

namespace MyAppTests.Logic.PersonMessageProvider;

class MyImplementationForTestAgeCalculator : AgeCalculator
{
    int preparedAge = 4;

    public void PrepareAge(int newAge)
    {
        preparedAge = newAge;
    }
    public Task<int> GetYearsFromDatesAsync(DateTime first, DateTime second)
    {
        return Task.FromResult(preparedAge);
    }
}

public class PersonAgeMessageProviderTests
{
    MyApp.Logic.PersonMessageProvider.PersonMessageProvider _sut;
    PersonDateOfBirthProvider dateOfBirthProvider;
    DateTimeProvider dateTimeProvider;
    ILogger<PersonAgeMessageProvider> logger;

    MyImplementationForTestAgeCalculator fakeCalculator = new MyImplementationForTestAgeCalculator();
    Person testPerson = new Person("Ist ja wurst", 7) { DateOfBirth = new DateTime( 1980, 1,1)};

    public PersonAgeMessageProviderTests()
    {
        PrepareFakes();
        _sut = new PersonAgeMessageProvider(fakeCalculator, dateOfBirthProvider, dateTimeProvider, logger);
    }

    private void PrepareFakes()
    {
        PreparePersonDateOfBirthProvider();
        PrepareDateTimeProvider();
        PrepareLogger();
    }

    private void PrepareLogger()
    {
        logger = A.Fake<ILogger<PersonAgeMessageProvider>>();
    }

    private void PrepareDateTimeProvider()
    {
        dateTimeProvider = A.Fake<DateTimeProvider>();
    }

    private void PreparePersonDateOfBirthProvider()
    {
        dateOfBirthProvider = A.Fake<PersonDateOfBirthProvider>();
        A.CallTo(() => dateOfBirthProvider.GetPersonDateOfBirthFromDatabaseOverTheInternetzzAsync(testPerson))
            .Returns(Task.FromResult(testPerson.DateOfBirth));
    }

    [Fact]
    public async Task ComposeMessageForPerson_WithValidPerson_ReturnsCorrectMessage()
    {
        var expectedAge = 18;
        fakeCalculator.PrepareAge(expectedAge);
        var result = await _sut.ComposeMessageForPerson(testPerson);
        result.Should().Be($"Ist ja wurst is {expectedAge} years old");
    }

    [Theory]
    [InlineData(4, "you shall not pass")]
    [InlineData(18, "Ist ja wurst is 18 years old")]
    public async Task AgeSpecificCompositionWorksAsIntended(int age, string expectedMessage)
    {
        fakeCalculator.PrepareAge(age);
        var result = await _sut.ComposeMessageForPerson(testPerson);
        result.Should().Be(expectedMessage);
    }
}