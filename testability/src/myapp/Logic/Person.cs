namespace MyApp.Logic;

public class Person
{
    public Person(string name, int id)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; }
    public string Name { get; }
    public DateTime DateOfBirth { get; init; }
}