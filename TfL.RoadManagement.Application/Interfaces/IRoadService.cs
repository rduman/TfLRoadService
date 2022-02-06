using TfL.RoadManagement.Application.Models;
using TfL.RoadManagement.TFL.Models;

namespace TfL.RoadManagement.Application.Interfaces;

public interface IRoadService
{
    Task<IList<RoadStatusResponse>> GetRoadService(RoadStatusRequest request);
}