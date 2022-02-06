using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using TfL.RoadManagement.Application.Infrastructure;
using Xunit;

namespace TfL.RoadManagement.Application.UnitTests.Infrastructure;

public class ServiceCollectionExtensionTests
{
    [Fact]
    public void AddTfLClient_WhenCalled_ConfiguresValidContainer()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddApplication();

        serviceCollection.Invoking(sc => sc.BuildServiceProvider(true)).Should().NotThrow();
    }

}