using MyApp.Logic;
using MyApp.Logic.PersonMessageProvider;
using MyApp.Model;

namespace MyAppTests.Logic.PersonMessageProvider;

public class DecoratedPersonAgeMessageProviderTests
{
    private readonly MyApp.Logic.PersonMessageProvider.PersonMessageProvider _sut;
    private readonly MyApp.Logic.AgeCalculator.AgeCalculator _ageCalculator;
    private readonly MyApp.Logic.PersonDateOfBirthProvider.PersonDateOfBirthProvider _dateOfBirthProvider;
    private readonly MyApp.Logic.DateTimeProvider.DateTimeProvider _dateTimeProvider;


    public DecoratedPersonAgeMessageProviderTests()
    {
        _ageCalculator = A.Fake<MyApp.Logic.AgeCalculator.AgeCalculator>();
        _dateOfBirthProvider = A.Fake<MyApp.Logic.PersonDateOfBirthProvider.PersonDateOfBirthProvider>();
        _dateTimeProvider = A.Fake<MyApp.Logic.DateTimeProvider.DateTimeProvider>();
        _sut = new DecoratedPersonAgeMessageProvider(_ageCalculator, _dateOfBirthProvider, _dateTimeProvider);
    }

    [Fact]
    public async Task ComposeMessageForPerson_GetAgeOfPerson()
    {
        Person maxi = new("Maxi", 4) { DateOfBirth = new DateTime(1981, 11, 3) };
        Person callArgument = null!;
        A.CallTo(() => _dateOfBirthProvider.GetPersonDateOfBirthFromDatabaseOverTheInternetzzAsync(A<Person>.Ignored))
            .Invokes(call =>
            {
                callArgument = (Person)call.Arguments[0]!;
            })
            .Returns(maxi.DateOfBirth);
        DateTime now = SetupDateTimeProvider();
        SetupAgeCalculator(now, 41);

        var result = await _sut.ComposeMessageForPerson(maxi);

        result.Should().Be($"******** {maxi.Name} is 41 years old ********");
        callArgument.Should().Be(maxi);
    }

    [Theory]
    [InlineData("***")]
    [InlineData("***33")]
    [InlineData("*##$**")]
    [InlineData("**S SD#*")]
    public async Task SettingUpDecorator_WorksAsExpected(string newDecorator)
    {
        Person maxi = new("Maxi", 4) { DateOfBirth = new DateTime(1981, 11, 3) };
        Person callArgument = null!;
        A.CallTo(() => _dateOfBirthProvider.GetPersonDateOfBirthFromDatabaseOverTheInternetzzAsync(A<Person>.Ignored))
            .Invokes(call =>
            {
                callArgument = (Person)call.Arguments[0]!;
            })
            .Returns(maxi.DateOfBirth);
        DateTime now = SetupDateTimeProvider();
        SetupAgeCalculator(now, 41);
        
        ((Decorator)_sut).SetupDecorator(newDecorator);

        var result = await _sut.ComposeMessageForPerson(maxi);

        result.Should().Be($"{newDecorator} {maxi.Name} is 41 years old {newDecorator}");
        callArgument.Should().Be(maxi);
    }

    private void SetupAgeCalculator(DateTime now, int returnedAge)
    {
        A.CallTo(() =>
                _ageCalculator.GetYearsFromDatesAsync(A<DateTime>.Ignored, A<DateTime>.That.Matches(dt => dt == now)))
            .Returns(returnedAge);
    }

    private DateTime SetupDateTimeProvider()
    {
        DateTime now = DateTime.Now;
        A.CallTo(() => _dateTimeProvider.Now).Returns(now);
        return now;
    }
}