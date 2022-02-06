using TfL.RoadManagement.Application.Models;
using TfL.RoadManagement.TFL.Models;

namespace TfL.RoadManagement.Application.Profile;

public class RoadServiceProfile : AutoMapper.Profile
{
    public RoadServiceProfile()
    {
        CreateMap<Road, RoadStatusResponse>().ReverseMap();

    }

}