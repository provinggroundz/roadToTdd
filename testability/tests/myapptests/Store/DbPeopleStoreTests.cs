using MyApp.Store;
using FluentAssertions;

namespace MyAppTests;

public class DbPeopleStoreTests
{
    PeopleStore _sut;

    public DbPeopleStoreTests()
    {
        _sut = new DbPeopleStore();
    }

    [Fact]
    public async Task GetPersonByIdAsync_WithId3_ShouldReturn_Metwally()
    {
        var person = await _sut.GetPersonByIdAsync(3);
        person.Name.Should().Be("Metwally Developier");
    }

    [Fact]
    public async Task GetPersonByIdAsync_WithId6_Should_ThrowException()
    {
        Action act = async () => await _sut.GetPersonByIdAsync(6);
        act.Should().Throw<Exception>();
    }
}
