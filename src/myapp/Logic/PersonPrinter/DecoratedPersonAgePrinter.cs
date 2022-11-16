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

    public async Task Print(string message)
    {
        EnsureTextWriterNotNull();
        await _textWriter?.WriteLineAsync( $"{_decorator} {message} {_decorator}")!;
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