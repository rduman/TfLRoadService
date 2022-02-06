using TfL.RoadManagement.TFL.Models;

namespace TfL.RoadManagement.TFL.Interfaces;

public interface IRoadProvider
{
    Task<IList<Road>> GetRoadStatus(RoadStatusRequest request);
}