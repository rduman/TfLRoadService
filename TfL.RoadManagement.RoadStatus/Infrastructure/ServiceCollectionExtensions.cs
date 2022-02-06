using Microsoft.Extensions.DependencyInjection;
using TfL.RoadManagement.Application.Infrastructure;
using TfL.RoadManagement.TFL.Configuration;

namespace TfL.RoadManagement.RoadStatus.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddApplication();

        services.AddTransient<ITfLConfiguration, TfLConfiguration>();
    }
}