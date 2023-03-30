using NUnit.Framework;
using RestAssured.Request.Builders;
using WireMock.Server;

namespace WireMockNetWorkshop
{
    public class TestBase
    {
        protected WireMockServer server;

        protected RequestSpecification requestSpec;

        [SetUp]
        public void StartServer()
        {
            server = WireMockServer.Start(9876);

            requestSpec = new RequestSpecBuilder()
                .WithHostName("localhost")
                .WithPort(9876)
                .Build();
        }

        [TearDown]
        public void StopServer()
        {
            server.Stop();
        }
    }
}
