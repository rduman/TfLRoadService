using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using AutoMapper;
using FluentAssertions;
using Moq;
using TfL.RoadManagement.Application.Models;
using TfL.RoadManagement.Application.Profile;
using TfL.RoadManagement.TFL.Interfaces;
using TfL.RoadManagement.TFL.Models;
using Xunit;

namespace TfL.RoadManagement.Application.UnitTests;

public class RoadServiceTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IRoadProvider> _mockRoadProvider;

    private readonly RoadService RoadService;

    public RoadServiceTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _mockRoadProvider = new Mock<IRoadProvider>();

        var config = new MapperConfiguration(cfg => cfg.AddProfile<RoadServiceProfile>());
        config.AssertConfigurationIsValid();
        var mapper = config.CreateMapper();

        RoadService = new RoadService(_mockRoadProvider.Object, mapper);
    }


    [Fact]
    public void Ctors_EnsureNotNullAndCorrectExceptionParameterName()
    {
        var assertion = new GuardClauseAssertion(_fixture);
        assertion.Verify(typeof(RoadService).GetConstructors());
    }

    [Fact]
    public async Task GetRoadService_WhenRequestIsNull_ShouldThrowArgumentNullException() =>
        await RoadService.Invoking(x => x.GetRoadService(null)).Should().ThrowAsync<ArgumentNullException>();

    [Fact]
    public async Task GetRoadService_WhenRoadIdsIsEmpty_ShouldThrowArgumentNullException() =>
        await RoadService.Invoking(x => x.GetRoadService(new RoadStatusRequest())).Should()
            .ThrowAsync<ArgumentNullException>();



    [Fact]
    public async Task GetRoadService_WhenRoadIdsResponseIsNotNull_ShouldReturnRoadInfo()
    {
        var request = _fixture.Create<RoadStatusRequest>();
        var response = _fixture.CreateMany<Road>().ToList();
        var expectedResponse = _fixture.CreateMany<RoadStatusResponse>().ToList();

        _mockRoadProvider.Setup(x => x.GetRoadStatus(It.IsAny<RoadStatusRequest>())).ReturnsAsync(response);

        var result = await RoadService.GetRoadService(request);

        result.ToList().Should().BeOfType<List<RoadStatusResponse>>();

    }

}