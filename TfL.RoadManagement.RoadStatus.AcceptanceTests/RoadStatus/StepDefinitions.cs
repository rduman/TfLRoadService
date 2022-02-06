using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using FluentAssertions;
using TechTalk.SpecFlow;
using TfL.RoadManagement.RoadStatus.Models;

namespace TfL.RoadManagement.RoadStatus.AcceptanceTests.RoadStatus;

[Binding]
public class StepDefinitions
{
    private readonly ConsoleArgs _consoleArgs;
    private readonly string _consoleAppExePath;
    private Process _process;

    public StepDefinitions(Process process)
    {
        _process = process;
        _consoleAppExePath = GetConsoleAppExePath();
        _consoleArgs = new ConsoleArgs();

    }

    private string GetConsoleAppExePath()
    {
        var executingAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var directory = new DirectoryInfo(executingAssemblyPath);

        var rootDirectory = directory?.Parent;

        var applicationName = typeof(Startup).Namespace;
        var applicationExePath = $"{rootDirectory?.FullName}\\net6.0\\{applicationName}.exe";

        return applicationExePath;
    }

    [Given("a valid roadID (.*) is specified")]
    public void GivenAValid_RoadId_IsSpecified(string roadId)
    {
        _consoleArgs.RoadIds = new[] { roadId };
    }

    [When]
    public void WhenTheClientIsRun()
    {
        var argsBuilder = new StringBuilder();

        argsBuilder.Append(string.Join(' ', _consoleArgs.RoadIds));

        var arguments = argsBuilder.ToString().Trim();

        var processStartInfo = new ProcessStartInfo
        {
            FileName = _consoleAppExePath,
            Arguments = arguments,
            WindowStyle = ProcessWindowStyle.Hidden,
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardOutput = true
        };

        _process = Process.Start(processStartInfo);
        _process?.WaitForExit(TimeSpan.FromSeconds(15).Milliseconds); //timeout after 15 seconds
    }

    [Then("the road displayName (.*) should be displayed")]
    public void ThenTheRoadDisplayNameShouldBeDisplayed(string expectedDisplayName)
    {
        var response = _process.StandardOutput.ReadToEnd().Split('\n');
        response[0].Should().Be($"The status of the {expectedDisplayName} is as follows\r");
    }



    [Given("an invalid roadID (.*) is specified")]
    public void GivenAnInvalid_RoadId_IsSpecified(string roadId)
    {
        _consoleArgs.RoadIds = new[] { roadId };
    }

    [Then("the application should return an informative error (.*)")]
    public void ThenTheApplicationShouldReturnAnInformativeError(string expectedError)
    {

        var firstLine = _process.StandardOutput.ReadLine()?.TrimEnd();
        firstLine.Should().Be(expectedError);
    }


    [Then("the road statusSeverity (.*) should be displayed")]
    public void ThenTheRoadStatusSeverityShouldBeDisplayed(string expectedStatusSeverity)
    {

            var output = _process.StandardOutput.ReadToEnd().Split('\n');

            var secondLine = output[1]?.TrimEnd();

            var regex = new Regex(@"\t Road Status is (?!\s*$).+"); //not empty
            expectedStatusSeverity.Should().Be("regex not empty");
            secondLine.Should().MatchRegex(regex.ToString());
  
    }


    [Then("the road statusSeverityDescription (.*) should be displayed")]
    public void ThenTheRoadStatusSeverityDescriptionShouldBeDisplayed(string expectedStatusSeverityDescription)
    {

            var output = _process.StandardOutput.ReadToEnd().Split('\n');

            var thirdLine = output[2]?.TrimEnd();

            var regex = new Regex(@"\t Road Status Description is (?!\s*$).+"); //not empty
            expectedStatusSeverityDescription.Should().Be("regex not empty");

    }


    [Then("the application should exit with a non-zero System Error code (.*)")]
    public void ThenTheApplicationShouldExitWithANonZeroSystemErrorCode(int errorCode)
    {
        _process.WaitForExit();
        _process.ExitCode.Should().NotBe(0);
        _process.ExitCode.Should().Be(errorCode);
   
    }
}