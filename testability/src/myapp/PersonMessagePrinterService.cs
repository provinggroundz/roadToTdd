using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using MyApp.Logic.PersonMessageProvider;
using MyApp.Logic.PersonPrinter;
using MyApp.Store;

namespace MyApp;

public class PersonMessagePrinterService : IHostedService
{
    private readonly ILogger<PersonMessagePrinterService> _logger;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly PeopleStore _peopleStore;
    private readonly PersonMessageProvider _personMessageProvider;
    private readonly TextWriterPrinter _textWriterPrinter;

    public PersonMessagePrinterService(ILogger<PersonMessagePrinterService> logger,IHostApplicationLifetime appLifetime, PeopleStore peopleStore, PersonMessageProvider personMessageProvider, TextWriterPrinter textWriterPrinter)
    {
        _logger = logger;
        _appLifetime = appLifetime;
        _peopleStore = peopleStore;
        _personMessageProvider = personMessageProvider;
        _textWriterPrinter = textWriterPrinter;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Worker Service Starting");
            await Run();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled Exception");
        }
        finally
        {
            _appLifetime.StopApplication();
        }
    }

    private async Task Run()
    {
        if (_personMessageProvider is Decorator decorator)
        {
            decorator.SetupDecorator("blublublu");
        }
        var message = await _personMessageProvider.ComposeMessageForPerson(await _peopleStore.GetPersonByIdAsync(3));
        
        await _textWriterPrinter.PrintByIdAsync(message, Console.Out);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
