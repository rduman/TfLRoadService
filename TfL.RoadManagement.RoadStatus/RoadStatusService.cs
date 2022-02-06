using Microsoft.Extensions.Hosting;
using TfL.RoadManagement.Application.Interfaces;
using TfL.RoadManagement.RoadStatus.Models;
using TfL.RoadManagement.TFL.Models;

namespace TfL.RoadManagement.RoadStatus;

public class RoadStatusService : BackgroundService
{
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly IRoadService _roadService;
    private readonly ConsoleArgs _consoleArgs;
    public RoadStatusService(IHostApplicationLifetime applicationLifetime,IRoadService roadService,ConsoleArgs consoleArgs)
    {
        _applicationLifetime = applicationLifetime ?? throw new ArgumentNullException(nameof(applicationLifetime));
        _roadService = roadService ?? throw new ArgumentNullException(nameof(roadService));
        _consoleArgs = consoleArgs ?? throw new ArgumentNullException(nameof(consoleArgs));

    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var request = new RoadStatusRequest
        {
            RoadIds = _consoleArgs.RoadIds
        };

        var result = await _roadService.GetRoadService(request);

        foreach (var roadStatusResponse in result)
        {
            Console.WriteLine($"The status of the {roadStatusResponse.DisplayName} is as follows");
            Console.WriteLine($"\t Road Status is {roadStatusResponse.StatusSeverity}");
            Console.WriteLine($"\t Road Status Description is {roadStatusResponse.StatusSeverityDescription}\n");
        }

        _applicationLifetime.StopApplication();
    }
}
