using NUnit.Framework;
using RestSharp;
using System.Net;
using System.Threading.Tasks;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace WireMockNetWorkshop.Answers
{
    public class Exercises01
    {
        private WireMockServer server;
        private RestClient client;

        private const string BASE_URL = "http://localhost:9876";

        [SetUp]
        public void StartServer()
        {
            server = WireMockServer.Start(9876);
            client = new RestClient(BASE_URL);
        }

        private void SetupStubExercise101()
        {
            /************************************************
		     * Create a stub that will respond to a POST
		     * to /requestLoan with an HTTP status code 200
		     ************************************************/

            server.Given(
                Request.Create().WithPath("/requestLoan").UsingPost()
            )
            .RespondWith(
                Response.Create()
                .WithStatusCode(200)
            );
        }

        private void SetupStubExercise102()
        {
            /************************************************
		     * Create a stub that will respond to a POST
		     * to /requestLoan with a response that contains
		     * a Content-Type header with value text/plain
		     ************************************************/

            server.Given(
                Request.Create().WithPath("/requestLoan").UsingPost()
            )
            .RespondWith(
                Response.Create()
                .WithHeader("Content-Type", "text/plain")
            );
        }

        private void SetupStubExercise103()
        {
            /************************************************
		     * Create a stub that will respond to a POST
		     * to /requestLoan with a plain text response body
		     * equal to 'Loan application received!'
		     ************************************************/

            server.Given(
                Request.Create().WithPath("/requestLoan").UsingPost()
            )
            .RespondWith(
                Response.Create()
                .WithBody("Loan application received!")
            );
        }

        [Test]
        public async Task TestStubExercise101()
        {
            SetupStubExercise101();

            RestRequest request = new RestRequest("/requestLoan", Method.Post);

            RestResponse response = await client.ExecuteAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task TestStubExercise102()
        {
            SetupStubExercise102();

            RestRequest request = new RestRequest("/requestLoan", Method.Post);

            RestResponse response = await client.ExecuteAsync(request);

            Assert.That(response.ContentType, Is.EqualTo("text/plain"));
        }

        [Test]
        public async Task TestStubExercise103()
        {
            SetupStubExercise103();

            RestRequest request = new RestRequest("/requestLoan", Method.Post);

            RestResponse response = await client.ExecuteAsync(request);

            Assert.That(response.Content, Is.EqualTo("Loan application received!"));
        }

        [TearDown]
        public void StopServer()
        {
            server.Stop();
        }
    }
}
