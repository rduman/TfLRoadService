using TfL.RoadManagement.TFL.Configuration;
using TfL.RoadManagement.TFL.Interfaces;

namespace TfL.RoadManagement.TFL;

public class UrlBuilder : IUrlBuilder
{
    private readonly ITfLConfiguration _tfLConfiguration;

    public UrlBuilder(ITfLConfiguration tfLConfiguration)
    {
        _tfLConfiguration = tfLConfiguration ?? throw new ArgumentNullException(nameof(tfLConfiguration));
    }

    public Uri BuildRoadId(IEnumerable<string> roadId)
    {
        var roadIds = string.Join(',', roadId);

        return new Uri(
            $"{_tfLConfiguration.ApiUrl}/Road/{roadIds}?app_id={_tfLConfiguration.AppId}&app_key={_tfLConfiguration.ApiKey}");
    }

}