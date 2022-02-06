
using Microsoft.Extensions.Configuration;

namespace TfL.RoadManagement.TFL.Configuration;

public class TfLConfiguration : ITfLConfiguration
{
    public TfLConfiguration()
    {
        
    }
    public TfLConfiguration(IConfiguration configuration)
    {
        if (configuration == null) throw new ArgumentNullException(nameof(configuration));

        configuration.Bind("TfLConfiguration", this);


    }

    public string ApiUrl { get; set; }
    public string AppId { get; set; }
    public string ApiKey { get; set; }
}