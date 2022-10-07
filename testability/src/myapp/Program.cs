// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

using MyApp;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Console()
    .WriteTo.File("myawesomelog.log")
    .CreateLogger();


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) => services.AddHostedService<Worker>())
    .UseSerilog()
    .Build();

await host.RunAsync();