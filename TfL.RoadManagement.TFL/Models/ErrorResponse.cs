using System.Net;

namespace TfL.RoadManagement.TFL.Models;

public class ErrorResponse
{
    public DateTimeOffset TimestampUtc { get; set; }
    public string ExceptionType { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }
    public string HttpStatus { get; set; }
    public string RelativeUri { get; set; }
    public string Message { get; set; }
}