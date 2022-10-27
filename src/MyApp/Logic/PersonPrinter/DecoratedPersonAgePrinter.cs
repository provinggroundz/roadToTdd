namespace MyApp.Logic.PersonPrinter;

public class DecoratedPersonAgePrinter : Printer, Decorator, TextWriterSetup
{
    private string _decorator;
    private TextWriter? _textWriter; 

    public DecoratedPersonAgePrinter()
    {
        _decorator = "#######";
    }

    public void SetupDecorator(string decorator)
    {
        _decorator = decorator;
    }

    public Task Print(string message)
    {
        EnsureTextWriterNotNull();
        _textWriter?.WriteLine( $"{_decorator} {message} {_decorator}");
        return Task.CompletedTask;
    }

    public void SetupTextWriter(TextWriter writer)
    {
        _textWriter = writer;
    }
    
    private void EnsureTextWriterNotNull()
    {
        if (_textWriter == null)
            throw new InvalidOperationException("TextWriter is not setup");
    }
}