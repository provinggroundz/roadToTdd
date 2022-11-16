using MyApp.Model;
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

    [Fact]
    public async Task GetPersonThatDoesNotExist_ThrowsException()
    {
        Person person;
        Func<Task> act = async () => person = await _sut.GetPersonByIdAsync(6);
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Sequence contains no matching element");
    }
}
