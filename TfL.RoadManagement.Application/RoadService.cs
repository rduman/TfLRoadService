using AutoMapper;
using TfL.RoadManagement.Application.Interfaces;
using TfL.RoadManagement.Application.Models;
using TfL.RoadManagement.TFL.Interfaces;
using TfL.RoadManagement.TFL.Models;

namespace TfL.RoadManagement.Application;

public class RoadService : IRoadService
{
    private readonly IRoadProvider _roadProvider;
    private readonly IMapper _mapper;
    public RoadService(IRoadProvider roadProvider, IMapper mapper)
    {
        _roadProvider = roadProvider ?? throw new ArgumentNullException(nameof(roadProvider));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IList<RoadStatusResponse>> GetRoadService(RoadStatusRequest request)
    {
        if (request == null || !request.RoadIds.Any()) throw new ArgumentNullException(nameof(request));

        var result = await _roadProvider.GetRoadStatus(request);

        return _mapper.Map<IList<RoadStatusResponse>>(result);

    }
}