using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace WireMockNetWorkshop.Examples
{
    [TestFixture]
    public class Examples02
    {
        private WireMockServer server;

        [SetUp]
        public void StartServer()
        {
            server = WireMockServer.Start(9876);
        }

        private void StubUrlMatching()
        {
            server.Given(
                Request.Create().WithPath("/hello-world").UsingGet()
            )
            .RespondWith(
                Response.Create()
                .WithBody("URL matching")
            );
        }

        [TearDown]
        public void StopServer()
        {
            server.Stop();
        }
    }
}
