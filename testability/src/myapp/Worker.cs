using Microsoft.Extensions.Hosting;

using MyApp.Logic;

namespace MyApp;

public class Worker : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        PersonAgePrinter printer = new();
        printer.PrintById(3);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
