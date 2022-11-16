using MyApp.Logic;
using MyApp.Logic.PersonPrinter;

namespace MyAppTests.Logic.PersonPrinter;

public class DecoratedPersonAgePrinterTests {
    Printer _sut;

    public DecoratedPersonAgePrinterTests()
    {
        _sut = new DecoratedPersonAgePrinter();
    }

    [Fact]
    public async Task PrintingWhileNotInitialized_ThrowsInvalidOperationException()
    {
        Func<Task> act = async () => await _sut.Print("blabla");
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("TextWriter is not setup");
    }

    [Fact]
    public async Task PrintingAfterInitialization_WorksAsIntended()
    {
        var fakeWriter = A.Fake<TextWriter>(opt => opt.Strict());
        string? actualArgument = null;
        A.CallTo(() => fakeWriter.WriteLineAsync(A<string?>.Ignored))
            .Invokes(call => actualArgument = (string)call.Arguments[0]!);
        ((TextWriterSetup)_sut).SetupTextWriter(fakeWriter);
        Func<Task> act = async () => await _sut.Print("blabla");
        await act.Should().NotThrowAsync();
        A.CallTo(() => fakeWriter.WriteLineAsync(A<string?>.Ignored)).MustHaveHappened();
        actualArgument.Should().NotBeNull();
        actualArgument.Should().Be("####### blabla #######");
    }

    [Theory]
    [InlineData("***")]
    [InlineData("234")]
    [InlineData("---")]
    [InlineData("- - - -")]
    public async Task ChangingDecoratorWorksAsIntended(string expectedDecorator)
    {
        var fakeWriter = A.Fake<TextWriter>(opt => opt.Strict());
        string? actualArgument = null;
        A.CallTo(() => fakeWriter.WriteLineAsync(A<string?>.Ignored))
            .Invokes(call => actualArgument = (string)call.Arguments[0]!);
        ((TextWriterSetup)_sut).SetupTextWriter(fakeWriter);
        ((Decorator)_sut).SetupDecorator(expectedDecorator);
        Func<Task> act = async () => await _sut.Print("blabla");
        await act.Should().NotThrowAsync();
        A.CallTo(() => fakeWriter.WriteLineAsync(A<string?>.Ignored)).MustHaveHappened();
        actualArgument.Should().NotBeNull();
        actualArgument.Should().Be($"{expectedDecorator} blabla {expectedDecorator}");
    }
}