using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using TfL.RoadManagement.TFL.Infrastructure;
using Xunit;

namespace TfL.RoadManagement.TFL.UnitTests.Infrastructure;

public class ServiceCollectionExtensionTests
{
    [Fact]
    public void AddTfLClient_WhenCalled_ConfiguresValidContainer()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddTfLClient();

        serviceCollection.Invoking(sc => sc.BuildServiceProvider(true)).Should().NotThrow();
    }

}