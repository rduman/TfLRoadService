using Microsoft.Extensions.DependencyInjection;
using TfL.RoadManagement.TFL.Interfaces;

namespace TfL.RoadManagement.TFL.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddTfLClient(this IServiceCollection services,
        Action<IHttpClientBuilder> clientBuilderDelegate = null)
    {
        var tflClientBuilder = services.AddHttpClient<IRoadClient, RoadClient>(
            builder => builder.DefaultRequestHeaders.Add("Accept", "application/json"));

        clientBuilderDelegate?.Invoke(tflClientBuilder);

        services.AddTransient<IRoadProvider, RoadProvider>();
        services.AddTransient<IUrlBuilder, UrlBuilder>();


    }

}