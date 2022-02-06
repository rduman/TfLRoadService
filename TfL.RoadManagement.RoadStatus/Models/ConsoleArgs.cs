using CommandLine;

namespace TfL.RoadManagement.RoadStatus.Models;

public class ConsoleArgs
{
    [Value(0, MetaName = nameof(RoadIds), Required = true)]
    public IEnumerable<string> RoadIds { get; set; }

}

