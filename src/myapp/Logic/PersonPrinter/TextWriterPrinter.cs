namespace MyApp.Logic.PersonPrinter;

public class TextWriterPrinter : Printer, TextWriterSetup
{
    private TextWriter? _textWriter; 
    public async Task Print(string message)
    {
        EnsureTextWriterNotNull();
        
        await _textWriter?.WriteLineAsync(message)!;
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