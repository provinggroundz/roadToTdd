using MyApp.Model;

namespace MyApp.Logic.PersonMessageProvider;

public interface PersonMessageProvider
{
    Task<string> ComposeMessageForPerson(Person person);
}
