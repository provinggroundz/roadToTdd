// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MyApp;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) => services.AddHostedService<Worker>())
    .Build();

host.RunAsync();