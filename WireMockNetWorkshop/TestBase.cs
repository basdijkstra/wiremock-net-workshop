using NUnit.Framework;
using RestSharp;
using WireMock.Server;

namespace WireMockNetWorkshop
{
    public class TestBase
    {
        protected WireMockServer server;
        protected RestClient client;

        private const string BASE_URL = "http://localhost:9876";

        [SetUp]
        public void StartServer()
        {
            server = WireMockServer.Start(9876);
            client = new RestClient(BASE_URL);
        }

        [TearDown]
        public void StopServer()
        {
            server.Stop();
        }
    }
}
