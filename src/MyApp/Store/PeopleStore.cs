using MyApp.Logic;
using MyApp.Model;

namespace MyApp.Store
{
    public interface PeopleStore
    {
        Task<Person> GetPersonByIdAsync(int id);
    }
}
