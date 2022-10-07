using MyApp.Logic;

namespace MyApp.Store
{
    public class PeopleStore
    {
        readonly List<Person> _people = new();

        public PeopleStore()
        {
            _people.Add(new Person("Baad Developier", 1) { DateOfBirth = new DateTime(1966,6,6)});
            _people.Add(new Person("Good Developier", 2) { DateOfBirth = new DateTime(1977,7,7)});
            _people.Add(new Person("Metwally Developier", 3) { DateOfBirth = new DateTime(1981,11,3)});
            _people.Add(new Person("Dave Developier", 4) { DateOfBirth = new DateTime(1988,8,8)});
        }

        public Task<Person> GetPersonByIdAsync(int id)
        {
            var person = _people.First(p => p.Id == id);
            return Task.FromResult(person);
        }
    }
}
