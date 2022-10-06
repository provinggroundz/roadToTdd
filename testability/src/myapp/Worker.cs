using Microsoft.Extensions.Hosting;

using MyApp.Logic;

namespace MyApp;

public class Worker : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        PersonAgePrinter printer = new();
        await printer.PrintByIdAsync(3);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
