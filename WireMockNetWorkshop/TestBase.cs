using NUnit.Framework;
using RestAssured.Request.Builders;
using RestSharp;
using WireMock.Server;

namespace WireMockNetWorkshop
{
    public class TestBase
    {
        protected WireMockServer server;
        protected RestClient client;

        protected RequestSpecification requestSpec;

        private const string BASE_URL = "http://localhost:9876";

        [SetUp]
        public void StartServer()
        {
            server = WireMockServer.Start(9876);
            client = new RestClient(BASE_URL);

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
