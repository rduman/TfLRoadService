using TfL.RoadManagement.TFL.Interfaces;
using TfL.RoadManagement.TFL.Models;

namespace TfL.RoadManagement.TFL;

public class RoadProvider : IRoadProvider
{
    private readonly IRoadClient _roadClient;

    public RoadProvider(IRoadClient roadClient)
    {
        _roadClient = roadClient ?? throw new ArgumentNullException(nameof(roadClient));
    }

    public async Task<IList<Road>> GetRoadStatus(RoadStatusRequest request)
    {
        if (request == null || !request.RoadIds.Any()) throw new ArgumentNullException(nameof(request));

        var result = await _roadClient.GetRoadStatus(request);

        return result;
    }
}