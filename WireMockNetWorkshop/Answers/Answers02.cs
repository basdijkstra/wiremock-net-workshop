using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace WireMockNetWorkshop.Answers
{
    public class Exercises02 : TestBase
    {
        private void SetupStubExercise201()
        {
            /************************************************
		     * Create a stub that will respond to all POST
		     * requests to /requestLoan with HTTP status code 503 
		     ************************************************/

            server.Given(
                Request.Create().WithPath("/requestLoan").UsingPost()
            )
            .RespondWith(
                Response.Create()
                .WithStatusCode(503)
            );
        }

        private void SetupStubExercise202()
        {
            /************************************************
		     * Create a stub that will respond to a POST
		     * request to /requestLoan, but only if this request
		     * contains a header 'speed' with value 'slow'.
		     * 
		     * Respond with status code 200, but only after a
		     * fixed delay of 3000 milliseconds.
		     ************************************************/

            server.Given(
                Request.Create().WithPath("/requestLoan").UsingPost()
                .WithHeader("speed", new ExactMatcher("slow"))
            )
            .RespondWith(
                Response.Create()
                .WithStatusCode(200)
                .WithDelay(TimeSpan.FromMilliseconds(3000))
            );
        }

        private void SetupStubExercise203()
        {
            /************************************************
		     * Create a stub that will respond to a POST request
		     * to /requestLoan, but only if this request contains
		     * a cookie 'session' with value 'invalid'
		     * 
		     * Respond with a Fault of type MALFORMED_RESPONSE_CHUNK
		     ************************************************/

            server.Given(
                Request.Create().WithPath("/requestLoan").UsingPost()
                .WithCookie("session", new ExactMatcher("invalid"))
            )
            .RespondWith(
                Response.Create()
                .WithFault(FaultType.MALFORMED_RESPONSE_CHUNK)
            );
        }

        private void SetupStubExercise204()
        {
            /************************************************
		     * Create a stub that will respond to a POST request
		     * to /requestLoan, but only if this request contains
		     * a JSON request body with an element 'status' with
		     * value 'active'.
		     * 
		     * Respond with an HTTP status code 201.
		     ************************************************/

            server.Given(
                Request.Create().WithPath("/requestLoan").UsingPost()
                .WithBody(new JmesPathMatcher("status == 'active'"))
            )
            .RespondWith(
                Response.Create()
                .WithStatusCode(201)
            );
        }

        [Test]
        public async Task TestStubExercise201()
        {
            SetupStubExercise201();

            RestRequest request = new RestRequest("/requestLoan", Method.Post);

            RestResponse response = await client.ExecuteAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.ServiceUnavailable));
        }

        [Test]
        public async Task TestStubExercise202()
        {
            SetupStubExercise202();

            RestRequest request = new RestRequest("/requestLoan", Method.Post);

            request.AddHeader("speed", "slow");

            Stopwatch stopwatch = Stopwatch.StartNew();

            RestResponse response = await client.ExecuteAsync(request);

            stopwatch.Stop();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(stopwatch.ElapsedMilliseconds, Is.GreaterThanOrEqualTo(3000));
        }

        [Test]
        public async Task TestStubExercise203()
        {
            SetupStubExercise203();

            RestRequest request = new RestRequest("/requestLoan", Method.Post);

            client.CookieContainer.Add(new Cookie("session", "invalid", "/requestLoan", "localhost"));

            RestResponse response = await client.ExecuteAsync(request);

            Assert.Throws<JsonReaderException>(() => JObject.Parse(response.Content!));
        }

        [Test]
        public async Task TestStubExercise204()
        {
            SetupStubExercise204();

            RestRequest request = new RestRequest("/requestLoan", Method.Post);

            request.AddJsonBody(new { status = "active" });

            RestResponse response = await client.ExecuteAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }
    }
}