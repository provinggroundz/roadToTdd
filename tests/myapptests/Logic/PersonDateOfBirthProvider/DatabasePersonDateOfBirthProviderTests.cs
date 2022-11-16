using MyApp.Logic.PersonDateOfBirthProvider;
using MyApp.Model;

namespace MyAppTests.Logic.PersonDateOfBirthProvider;

public class DatabasePersonDateOfBirthProviderTests
{
    private MyApp.Logic.PersonDateOfBirthProvider.PersonDateOfBirthProvider _sut;

    public DatabasePersonDateOfBirthProviderTests()
    {
        _sut = new DatabasePersonDateOfBirthProvider();
    }

    [Fact]
    public async Task GetPersonDateOfBirth_WorksAssIntended()
    {
        Person maxi = new("Maxi", 4) { DateOfBirth = new DateTime(1981, 11, 3) };
        var result = await _sut.GetPersonDateOfBirthFromDatabaseOverTheInternetzzAsync(maxi);
        result.Should().Be(maxi.DateOfBirth);
    }
}