using MyApp.Logic;

namespace MyApptests;

public class PersonAgePrinterTests
{
    private readonly PersonAgePrinter _sut;
    private readonly MemoryStream _memoryStream;
    private readonly StreamWriter _testWriter;

    public PersonAgePrinterTests()
    {
       _sut = new();
       _memoryStream = new MemoryStream();
       _testWriter = new(_memoryStream);
       _testWriter.AutoFlush = true;
       Console.SetOut(_testWriter);
    }

    [Fact]
    public void Print_Max_PrintsOut40()
    {
        _sut.PrintById(3);
        _memoryStream.Seek(0, SeekOrigin.Begin);
        using(StreamReader reader = new (_memoryStream))
        {
            var line = reader.ReadLine();
            System.Console.WriteLine(line);
        }
    }
}