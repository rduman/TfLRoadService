using System.Net;
using Newtonsoft.Json;
using TfL.RoadManagement.TFL.Exceptions;
using TfL.RoadManagement.TFL.Models;

namespace TfL.RoadManagement.TFL;

public static class ExceptionBuilder
{

    public static async Task ErrorHandler(HttpResponseMessage responseMessage, IEnumerable<string> message)
    {

        var content = await responseMessage.Content.ReadAsStringAsync();

        var apiError = JsonConvert.DeserializeObject<ErrorResponse>(content);

        var roadIdsNotFound = message
            .Where(r => apiError != null && apiError.Message.Contains(r)).ToList();

        switch (responseMessage.StatusCode)
        {
            case HttpStatusCode.NotFound:
                throw new NotFoundException(typeof(Road), roadIdsNotFound);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}