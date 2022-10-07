namespace MyApp.Logic
{
    public interface IDateTimeProvider
    {
         public DateTime Now { get; }
    }

    public class CurrentDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}