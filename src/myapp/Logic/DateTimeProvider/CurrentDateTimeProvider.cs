namespace MyApp.Logic.DateTimeProvider;

public class CurrentDateTimeProvider : DateTimeProvider
{
    public DateTime Now => DateTime.Now;
}