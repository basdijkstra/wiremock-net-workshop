using NUnit.Framework;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace WireMockNetWorkshop.Examples
{
    [TestFixture]
    public class Examples04
    {
        private WireMockServer server;

        [SetUp]
        public void StartServer()
        {
            server = WireMockServer.Start(9876);
        }

        private void CreateStubEchoHttpMethod()
        {
            server.Given(
                Request.Create().WithPath("/echo-http-method").UsingAnyMethod()
            )
            .RespondWith(
                Response.Create()
                .WithStatusCode(200)
                // The {{request.method}} handlebar extracts the HTTP method from the request
                .WithBody("HTTP method used was {{request.method}}")
                // This enables response templating for this specific mock response
                .WithTransformer()
            );
        }

        private void CreateStubEchoJsonRequestElement()
        {
            server.Given(
                Request.Create().WithPath("/echo-json-request-element").UsingPost()
            )
            .RespondWith(
                Response.Create()
                .WithStatusCode(200)
                // This extracts the book.title element from the JSON request body
                // (using a JsonPath expression) and repeats it in the response body
                .WithBody("The specified book title is {{JsonPath.SelectToken request.body \"$.book.title\"}}")
                // This enables response templating for this specific mock response
                .WithTransformer()
            );
        }

        [TearDown]
        public void StopServer()
        {
            server.Stop();
        }
    }
}
