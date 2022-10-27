namespace MyApp.Logic.PersonPrinter;

public interface TextWriterPrinter
{
    Task PrintByIdAsync(string message, TextWriter console);
}