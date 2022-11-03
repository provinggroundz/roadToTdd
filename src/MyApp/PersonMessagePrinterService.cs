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
    private readonly Printer _printer;

    public PersonMessagePrinterService(ILogger<PersonMessagePrinterService> logger,
        IHostApplicationLifetime appLifetime,
        PeopleStore peopleStore, 
        PersonMessageProvider personMessageProvider, 
        Printer printer)
    {
        _logger = logger;
        _appLifetime = appLifetime;
        _peopleStore = peopleStore;
        _personMessageProvider = personMessageProvider;
        _printer = printer;
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
            decorator.SetupDecorator("blublublu");
        var message = await _personMessageProvider.ComposeMessageForPerson(await _peopleStore.GetPersonByIdAsync(3));
        if(_printer is TextWriterSetup writerSetup)
            writerSetup.SetupTextWriter(Console.Out);
        await _printer.Print(message);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
