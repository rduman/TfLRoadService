using System.Collections.Generic;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using TfL.RoadManagement.TFL.Configuration;
using Xunit;

namespace TfL.RoadManagement.TFL.UnitTests.Infrastructure;

public class TfLConfigurationTests
{
    private readonly IFixture _fixture;
    private readonly Dictionary<string, string> _stubConfigs;
    private readonly IConfigurationBuilder _configurationBuilder;

    private TfLConfiguration StorageConfiguration => new(_configurationBuilder.Build());

    public TfLConfigurationTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _stubConfigs = new Dictionary<string, string>();
        _configurationBuilder = new ConfigurationBuilder().AddInMemoryCollection(_stubConfigs);
    }

    [Fact]
    public void Ctors_EnsureNotNullAndCorrectExceptionParameterName()
    {
        var assertion = new GuardClauseAssertion(_fixture);
        assertion.Verify(typeof(TfLConfiguration).GetConstructors());
    }

    [Fact]
    public void Get_Storage_ApiUrl()
    {
        var apiUrl = _fixture.Create<string>();
        _stubConfigs.Add("TfLConfiguration:ApiUrl", apiUrl);

        var result = StorageConfiguration.ApiUrl;

        result.Should().Be(apiUrl);
    }

    [Fact]
    public void Get_Storage_AppId()
    {
        var appId = _fixture.Create<string>();
        _stubConfigs.Add("TfLConfiguration:AppId", appId);

        var result = StorageConfiguration.AppId;

        result.Should().Be(appId);
    }

    [Fact]
    public void Get_Storage_ApiKey()
    {
        var apiKey = _fixture.Create<string>();
        _stubConfigs.Add("TfLConfiguration:ApiKey", apiKey);

        var result = StorageConfiguration.ApiKey;

        result.Should().Be(apiKey);
    }
}