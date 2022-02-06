namespace TfL.RoadManagement.TFL.Configuration;

public interface ITfLConfiguration
{
    public string ApiUrl { get; set; }
    public string AppId { get; set; }
    public string ApiKey { get; set; }
}