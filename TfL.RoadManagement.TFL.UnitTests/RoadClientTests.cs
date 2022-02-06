using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using TfL.RoadManagement.TestDataBuilders;
using TfL.RoadManagement.TFL.Configuration;
using TfL.RoadManagement.TFL.Exceptions;
using TfL.RoadManagement.TFL.Interfaces;
using TfL.RoadManagement.TFL.Models;
using Xunit;

namespace TfL.RoadManagement.TFL.UnitTests;

public class RoadClientTests
{
    private readonly IFixture _fixture;
    private readonly Mock<ITfLConfiguration> _mockTfLConfiguration;
    private readonly Mock<ILogger<RoadClient>> _mockLogger;
    private readonly Mock<IUrlBuilder> _mockUrlBuilder;
    private HttpClient _httpClient;

    private RoadClient RoadClient =>
        new(_httpClient, _mockTfLConfiguration.Object, _mockUrlBuilder.Object, _mockLogger.Object);

    public RoadClientTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());

        _mockTfLConfiguration = new Mock<ITfLConfiguration>();
        _mockTfLConfiguration = new Mock<ITfLConfiguration>();
        _mockUrlBuilder = new Mock<IUrlBuilder>();
        _mockLogger = new Mock<ILogger<RoadClient>>();

        _httpClient = new TestHttpClientBuilder().WithStatusCode(HttpStatusCode.OK).Build();

        _mockTfLConfiguration.Setup(x => x.ApiUrl).Returns("https://api.tfl.gov.uk/");
        _mockTfLConfiguration.Setup(x => x.ApiKey).Returns("ApiKey");
        _mockTfLConfiguration.Setup(x => x.AppId).Returns("AppId");

    }

    [Fact]
    public void Ctors_EnsureNotNullAndCorrectExceptionParameterName()
    {
        var assertion = new GuardClauseAssertion(_fixture);
        assertion.Verify(typeof(RoadClient).GetConstructors());
    }

    [Fact]
    public async Task GetRoadStatus_WhenRoadIdNotFound_ShouldThrowsNotFoundException()
    {
        var request = _fixture.Create<RoadStatusRequest>();
        var expectedResponse = _fixture.Create<ErrorResponse>();

        var expectedUri = _fixture.Create<Uri>();

        _mockUrlBuilder.Setup(x => x.BuildRoadId(It.IsAny<IEnumerable<string>>())).Returns(expectedUri);

        _httpClient = new TestHttpClientBuilder().WithStatusCode(HttpStatusCode.NotFound).WithJsonContent(expectedResponse).Build();

        await RoadClient.Invoking(x => x.GetRoadStatus(request)).Should()
            .ThrowAsync<NotFoundException>();

    }

    [Fact]
    public async Task GetRoadStatus_WhenRoadIdFound_ShouldReturnRoadInfo()
    {
        var request = _fixture.Create<RoadStatusRequest>();
        var expectedResponse = _fixture.Create<IList<Road>>();

        var expectedUri = _fixture.Create<Uri>();

        _mockUrlBuilder.Setup(x => x.BuildRoadId(It.IsAny<IEnumerable<string>>())).Returns(expectedUri);


        _httpClient = new TestHttpClientBuilder().WithStatusCode(HttpStatusCode.OK).WithJsonContent(expectedResponse).Build();

        var response = await RoadClient.GetRoadStatus(request);

        response.Should().BeEquivalentTo(expectedResponse);
    }

}

