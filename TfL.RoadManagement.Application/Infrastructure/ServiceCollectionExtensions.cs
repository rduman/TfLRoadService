using Microsoft.Extensions.DependencyInjection;
using TfL.RoadManagement.Application.Interfaces;
using TfL.RoadManagement.TFL.Infrastructure;

namespace TfL.RoadManagement.Application.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddTfLClient();

        services.AddTransient<IRoadService, RoadService>();
    }
}