using System.IO;
using System.Reflection;
using BoDi;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;
using TfL.RoadManagement.TFL.Configuration;

namespace TfL.RoadManagement.RoadStatus.AcceptanceTests;

[Binding]
public sealed class SpecflowSetup
{
    private readonly IObjectContainer _objectContainer;

    public SpecflowSetup(IObjectContainer objectContainer)
    {
        _objectContainer = objectContainer;
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        var rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var appSettingsPath = Path.Combine(rootDirectory, "appsettings.acceptancetests.json");

        var builder = new ConfigurationBuilder()
            .AddJsonFile(appSettingsPath);

        var configurationRoot = builder.Build();
        var appConfig = configurationRoot.Get<TfLConfiguration>();

        _objectContainer.RegisterInstanceAs(appConfig);
    }

    [AfterScenario]
    public void AfterScenario()
    {
    }
}

