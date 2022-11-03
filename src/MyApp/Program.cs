// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.Hosting;
using Serilog;

using MyApp;
using MyApp.Logic.AgeCalculator;
using MyApp.Logic.DateTimeProvider;
using MyApp.Logic.PersonDateOfBirthProvider;
using MyApp.Logic.PersonMessageProvider;
using MyApp.Logic.PersonPrinter;
using MyApp.Store;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Console()
    .WriteTo.File("myawesomelog.log")
    .CreateLogger();


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddHostedService<PersonMessagePrinterService>()
            .AddScoped<PeopleStore, FakeDbPeopleStore>()
            .AddScoped<Printer, TextWriterPrinter>()
            .AddScoped<AgeCalculator, MakeThemOldAgeCalculator>()
            .AddScoped<DateTimeProvider, CurrentDateTimeProvider>()
            .AddScoped<PersonDateOfBirthProvider, DatabasePersonDateOfBirthProvider>()
            .AddScoped<PersonMessageProvider, DecoratedPersonAgeMessageProvider>();
    })
    .UseSerilog()
    .Build();

await host.RunAsync();