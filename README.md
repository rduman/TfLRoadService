

## 1- Download and install.

- [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) SDK and .NET Runtime. _(Required)_
- Visual Studio 2022 [Visual Studio](https://visualstudio.microsoft.com/vs/community/)  _(Required)_



## 2- Build Project with Visual Studio

- Add your AppId and ApiKey to the "appsettings.json" file in the TfL.RoadManagement\TfL.RoadManagement.RoadStatus
- Build TfL.RoadManagement.RoadStatus console app in "Debug" mode


## 3- Samples

## Valid Road
- Open command line and pass this path
	`TfL.RoadManagement\TfL.RoadManagement.RoadStatus\bin\Debug\net6.0\TfL.RoadManagement.RoadStatus.exe A2`

### Response:
            The status of the A2 is as follows
                     Road Status is Good
                     Road Status Description is No Exceptional Delays



## Invalid Road
                     - Open command line and pass this path
	`TfL.RoadManagement\TfL.RoadManagement.RoadStatus\bin\Debug\net6.0\TfL.RoadManagement.RoadStatus.exe A233`

### Response:
            fail: Microsoft.Extensions.Hosting.Internal.Host[9]
                  BackgroundService failed
                  TfL.RoadManagement.TFL.Exceptions.NotFoundException: 'A233 is not a valid road'
                     at TfL.RoadManagement.TFL.ExceptionBuilder.ErrorHandler(HttpResponseMessage responseMessage, IEnumerable`1 message) in \TfL.RoadManagement\TfL.RoadManagement.TFL\ExceptionBuilder.cs:line 24
                     at TfL.RoadManagement.TFL.RoadClient.GetRoadStatus(RoadStatusRequest request) in \TfL.RoadManagement\TfL.RoadManagement.TFL\RoadClient.cs:line 39
                     at TfL.RoadManagement.TFL.RoadProvider.GetRoadStatus(RoadStatusRequest request) in \TfL.RoadManagement\TfL.RoadManagement.TFL\RoadProvider.cs:line 19
                     at TfL.RoadManagement.Application.RoadService.GetRoadService(RoadStatusRequest request) in \TfL.RoadManagement\TfL.RoadManagement.Application\RoadService.cs:line 23
                     at TfL.RoadManagement.RoadStatus.RoadStatusService.ExecuteAsync(CancellationToken stoppingToken) in \TfL.RoadManagement\TfL.RoadManagement.RoadStatus\RoadStatusService.cs:line 28
                     at Microsoft.Extensions.Hosting.Internal.Host.TryExecuteBackgroundServiceAsync(BackgroundService backgroundService)


Notes :  
I added a exception handler for console app to Startup.ConfigureServices `AppDomain.CurrentDomain.UnhandledException += ConsoleExceptionHandler.HandleException;` 
But It wasn't triggered by the console app. I made some configuration for .Net 6 but I couldn't trigger the exception handler. I tried to add IApplicationBuilder then couldn't inject to the HostBuilder.
