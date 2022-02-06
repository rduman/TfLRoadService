using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace TfL.RoadManagement.TestDataBuilders;

public class TestHttpClientBuilder
{
    private readonly HttpResponseMessage _stubHttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);

    public TestHttpClientBuilder WithStatusCode(HttpStatusCode statusCode)
    {
        _stubHttpResponseMessage.StatusCode = statusCode;
        return this;
    }

    public TestHttpClientBuilder WithJsonContent<T>(T expectedResponseObject)
    {
        _stubHttpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(expectedResponseObject), Encoding.UTF8, "application/json");
        return this;
    }

    public TestHttpClient Build()
    {
        return new TestHttpClient(
            new FakeHttpMessageHandler(_stubHttpResponseMessage));
    }

    public class TestHttpClient : HttpClient
    {
        private readonly FakeHttpMessageHandler _httpMessageHandler;

        internal TestHttpClient(FakeHttpMessageHandler httpMessageHandler) : base(httpMessageHandler)
        {
            _httpMessageHandler = httpMessageHandler;
            BaseAddress = new Uri("http://localhost.com");
        }

        public IReadOnlyList<HttpRequestMessage> CapturedRequests => _httpMessageHandler.CapturedRequests;
    }
}