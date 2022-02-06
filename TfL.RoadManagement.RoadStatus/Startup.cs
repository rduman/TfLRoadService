using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TfL.RoadManagement.Application.Profile;
using TfL.RoadManagement.RoadStatus.Infrastructure;

namespace TfL.RoadManagement.RoadStatus;

public class Startup
{
    private IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {

        services.AddHostedService<RoadStatusService>();

        services.AddServices();

        services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddLogging(l =>
            l.AddConfiguration(Configuration.GetSection("Logging")));


        services.AddAutoMapper(c =>
            c.AddProfile<RoadServiceProfile>());

        AppDomain.CurrentDomain.UnhandledException += ConsoleExceptionHandler.HandleException;


    }

    public void Configure(IConfigurationBuilder app, IHostEnvironment env)
    {
        app.AddEnvironmentVariables();
        app.AddJsonFile("appsettings.json", false);
    }



}