namespace TfL.RoadManagement.TFL.Interfaces;

public interface IUrlBuilder
{
    Uri BuildRoadId(IEnumerable<string> roadId);
}