using NUnit.Framework;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace WireMockNetWorkshop.Examples
{
    [TestFixture]
    public class Examples01
    {
        private WireMockServer server;

        [SetUp]
        public void StartServer()
        {
            server = WireMockServer.Start(9876);
        }

        private void CreateHelloWorldStub()
        {
            server.Given(
                Request.Create().UsingGet().WithPath("/hello-world")
            )
            .RespondWith(
                Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "text/plain")
                .WithBody("Hello, world!")
            );
        }

        [TearDown]
        public void StopServer()
        {
            server.Stop();
        }
    }
}
