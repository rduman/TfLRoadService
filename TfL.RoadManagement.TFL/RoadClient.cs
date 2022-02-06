using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TfL.RoadManagement.TFL.Configuration;
using TfL.RoadManagement.TFL.Interfaces;
using TfL.RoadManagement.TFL.Models;

namespace TfL.RoadManagement.TFL;

public class RoadClient :IRoadClient
{
    private readonly ITfLConfiguration _tfLConfiguration;
    private readonly HttpClient _httpClient;
    private readonly ILogger<RoadClient> _logger;
    private readonly IUrlBuilder _urlBuilder;
    public RoadClient(HttpClient httpClient, ITfLConfiguration tfLConfiguration, IUrlBuilder urlBuilder,  ILogger<RoadClient> logger)
    {
        _tfLConfiguration = tfLConfiguration ?? throw new ArgumentNullException(nameof(tfLConfiguration));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _urlBuilder= urlBuilder ?? throw new ArgumentNullException(nameof(urlBuilder));

        _httpClient.BaseAddress = new Uri(_tfLConfiguration.ApiUrl);

    }
    public async Task<IList<Road>> GetRoadStatus(RoadStatusRequest request)
    {

        var requestUrl = _urlBuilder.BuildRoadId(request.RoadIds);
        var responseMessage = await _httpClient.GetAsync(requestUrl);

        var content = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode)
        {
            var apiError = JsonConvert.DeserializeObject<ErrorResponse>(content);

            var roadIdsNotFound = request.RoadIds
                .Where(r => apiError != null && apiError.Message.Contains(r)).ToList();

            await ExceptionBuilder.ErrorHandler(responseMessage, roadIdsNotFound);
        }

        var result = await responseMessage.Content.ReadAsAsync<IList<Road>>();

        return result;


    }
}