using MyApp.Logic;

public interface IPeopleStore
{
    Task<Person> GetPersonByIdAsync(int id);
}