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
                Request.Create().UsingAnyMethod().WithPath("/echo-http-method")
            )
            .RespondWith(
                Response.Create()
                .WithStatusCode(200)
                .WithBody("HTTP method used was {{request.method}}")
                .WithTransformer()
            );
        }

        private void CreateStubEchoJsonRequestElement()
        {
            server.Given(
                Request.Create().UsingPost().WithPath("/echo-json-request-element")
            )
            .RespondWith(
                Response.Create()
                .WithStatusCode(200)
                .WithBody("The specified book title is {{JsonPath.SelectToken request.body \"$.book.title\"}}")
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
