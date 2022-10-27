namespace MyApp.Logic.PersonPrinter;

public class DefaultTextWriterPrinter : TextWriterPrinter
{
    public Task PrintByIdAsync(string message, TextWriter console)
    {
        console.WriteLine(message);
        return Task.CompletedTask;
    }
}