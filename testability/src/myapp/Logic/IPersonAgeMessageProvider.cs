namespace MyApp.Logic;

public interface IPersonAgeMessageProvider
{
    Task<string> ComposeBirthdayMessageForPerson(Person person);
}
