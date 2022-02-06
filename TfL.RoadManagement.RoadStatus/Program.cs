// See https://aka.ms/new-console-template for more information

using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TfL.RoadManagement.RoadStatus;
using TfL.RoadManagement.RoadStatus.Models;

var argsParser = new Parser(with => with.CaseSensitive = false);


var parserResult = argsParser.ParseArguments<ConsoleArgs>(args);
await parserResult.WithParsedAsync(RunAsync);


static async Task RunAsync(ConsoleArgs args)
{
    
    using var host = new HostBuilder()
        
        .ConfigureAppConfiguration((hostContext, configBuilder) =>
        {
            
            new Startup(hostContext.Configuration)
                .Configure(configBuilder, hostContext.HostingEnvironment);

        })
        .ConfigureServices((hostContext, serviceCollection) =>
        {
            
            new Startup(hostContext.Configuration)
                .ConfigureServices(serviceCollection);

            serviceCollection.AddSingleton(new ConsoleArgs() {RoadIds = args.RoadIds });

            
            serviceCollection.Configure<HostOptions>(hostOptions =>
            {
                hostOptions.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
            });

            
        })
        
        .ConfigureLogging(builder => builder.AddConsole())
        .UseConsoleLifetime()
        .Build();

    
    
    await host.RunAsync();
}
