using MyApp.Logic;

namespace MyApptests;

public class PersonAgePrinterTests
{
    private readonly PersonAgePrinter _sut;
    private readonly MemoryStream _memoryStream;
    private readonly StreamWriter _testWriter;
    private readonly TextWriter _oldConsole;

    public PersonAgePrinterTests()
    {
       _sut = new();
       _memoryStream = new MemoryStream();
       _testWriter = new(_memoryStream);
       _testWriter.AutoFlush = true;
       _oldConsole = Console.Out;
       Console.SetOut(_testWriter);
    }

    [Fact]
    public async Task Print_Max_PrintsOut40()
    {
        await _sut.PrintByIdAsync(3);
        _memoryStream.Seek(0, SeekOrigin.Begin);
        using(StreamReader reader = new (_memoryStream))
        {
            var line = reader.ReadLine();
            _oldConsole.WriteLine(line);
        }
    }
}