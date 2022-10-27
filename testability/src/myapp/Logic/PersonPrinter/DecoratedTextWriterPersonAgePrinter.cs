namespace MyApp.Logic.PersonPrinter;

public class DecoratedTextWriterPersonAgePrinter : TextWriterPrinter, Decorator
{
    private string _decorator;

    public DecoratedTextWriterPersonAgePrinter()
    {
        _decorator = "#######";
    }

    public void SetupDecorator(string decorator)
    {
        _decorator = decorator;
    }

    public Task PrintByIdAsync(string message, TextWriter console)
    {
        console.WriteLine( $"{_decorator} {message} {_decorator}");
        return Task.CompletedTask;
    }
}