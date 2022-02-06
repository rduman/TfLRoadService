namespace TfL.RoadManagement.TestDataBuilders;

public class FakeHttpMessageHandler : HttpMessageHandler
{
    private readonly HttpResponseMessage _response;
    private readonly List<HttpRequestMessage> _capturedRequests = new List<HttpRequestMessage>();

    public FakeHttpMessageHandler(HttpResponseMessage response)
    {
        _response = response;
    }

    public IReadOnlyList<HttpRequestMessage> CapturedRequests => _capturedRequests;

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        _capturedRequests.Add(request);

        return Task.FromResult(_response);
    }
}