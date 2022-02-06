using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using FluentAssertions;
using Moq;
using TfL.RoadManagement.TFL.Interfaces;
using TfL.RoadManagement.TFL.Models;
using Xunit;

namespace TfL.RoadManagement.TFL.UnitTests;

public class RoadProviderTests
{
    private IFixture _fixture;
    private readonly Mock<IRoadClient> _mockRoadClient;

    private RoadProvider RoadProvider => new RoadProvider(_mockRoadClient.Object);
    public RoadProviderTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _mockRoadClient = new Mock<IRoadClient>();

    }

    [Fact]
    public void Ctors_EnsureNotNullAndCorrectExceptionParameterName()
    {
        var assertion = new GuardClauseAssertion(_fixture);
        assertion.Verify(typeof(RoadProvider).GetConstructors());
    }

    [Fact]
    public async Task GetRoadStatus_WhenRequestIsNull_ShouldThrowArgumentNullException() =>
        await RoadProvider.Invoking(x => x.GetRoadStatus(null)).Should().ThrowAsync<ArgumentNullException>();

    [Fact]
    public async Task GetRoadStatus_WhenRoadIdsIsEmpty_ShouldThrowArgumentNullException() =>
        await RoadProvider.Invoking(x => x.GetRoadStatus(new RoadStatusRequest())).Should()
            .ThrowAsync<ArgumentNullException>();

    [Fact]
    public async Task GetRoadStatus_WhenRoadsIdIsFound_ShouldReturnRoadsInfo()
    {
        var request = _fixture.Create<RoadStatusRequest>();
        var expectedResult = _fixture.CreateMany<Road>().ToList();
        _mockRoadClient.Setup(x => x.GetRoadStatus(It.IsAny<RoadStatusRequest>())).ReturnsAsync(expectedResult);

        var result = await RoadProvider.GetRoadStatus(request);

        result.Should().BeEquivalentTo(expectedResult);
    }
}