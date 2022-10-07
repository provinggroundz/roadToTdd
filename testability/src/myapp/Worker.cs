using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using MyApp.Logic;

namespace MyApp;

public class Worker : IHostedService
{
    private readonly ILogger<Worker> _logger;
    private readonly IHostApplicationLifetime _appLifetime;

    public Worker(ILogger<Worker> logger,IHostApplicationLifetime appLifetime)
    {
        _logger = logger;
        _appLifetime = appLifetime;
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

    private static async Task Run()
    {
        IPeopleStore store;
        IPersonAgeMessageProvider messageProvider;

        var message = await messageProvider.ComposeBirthdayMessageForPerson(await store.GetPersonByIdAsync(3));
        PersonAgePrinter printer = new();
        await printer.PrintByIdAsync(3);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
