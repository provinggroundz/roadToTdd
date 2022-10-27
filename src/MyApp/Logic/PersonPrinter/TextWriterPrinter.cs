namespace MyApp.Logic.PersonPrinter;

public class TextWriterPrinter : Printer, TextWriterSetup
{
    private TextWriter? _textWriter;
    public Task Print(string message)
    {
        EnsureTextWriterNotNull();
        _textWriter?.WriteLine(message);
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