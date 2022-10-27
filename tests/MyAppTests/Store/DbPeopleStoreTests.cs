using FluentAssertions;

using MyApp.Store;

namespace MyAppTests.Store;

public class DbPeopleStoreTests
{
    PeopleStore _sut;

    public DbPeopleStoreTests()
    {
        _sut = new FakeDbPeopleStore();
    }

    [Fact]
    public async Task GetPersonByIdAsync_WithId3_ShouldReturn_Metwally()
    {
        var person = await _sut.GetPersonByIdAsync(3);
        person.Name.Should().Be("Metwally Developier");
    }
}
