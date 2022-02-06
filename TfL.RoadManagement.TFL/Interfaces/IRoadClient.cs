using TfL.RoadManagement.TFL.Models;

namespace TfL.RoadManagement.TFL.Interfaces;

public interface IRoadClient
{
    Task<IList<Road>> GetRoadStatus(RoadStatusRequest request);
}