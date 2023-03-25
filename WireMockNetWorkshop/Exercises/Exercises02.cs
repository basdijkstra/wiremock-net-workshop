using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

using static RestAssured.Dsl;

namespace WireMockNetWorkshop.Exercises
{
    public class Exercises02 : TestBase
    {
        private void SetupStubExercise201()
        {
            /************************************************
		     * Create a stub that will respond to all POST
		     * requests to /requestLoan with HTTP status code 503 
		     ************************************************/

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

        }

        [Test]
        public void TestStubExercise201()
        {
            SetupStubExercise201();

            Given()
                .Spec(this.requestSpec)
                .When()
                .Post("/requestLoan")
                .Then()
                .StatusCode(HttpStatusCode.ServiceUnavailable);
        }

        [Test]
        public void TestStubExercise202()
        {
            SetupStubExercise202();

            Stopwatch stopwatch = Stopwatch.StartNew();

            Given()
                .Spec(this.requestSpec)
                .Header("speed", "slow")
                .When()
                .Post("/requestLoan")
                .Then()
                .StatusCode(HttpStatusCode.OK);

            stopwatch.Stop();

            Assert.That(stopwatch.ElapsedMilliseconds, Is.GreaterThanOrEqualTo(3000));

            stopwatch = Stopwatch.StartNew();

            Given()
                .Spec(this.requestSpec)
                .When()
                .Post("/requestLoan")
                .Then()
                .StatusCode(HttpStatusCode.NotFound);

            stopwatch.Stop();

            Assert.That(stopwatch.ElapsedMilliseconds, Is.LessThanOrEqualTo(1000));
        }

        [Test]
        public void TestStubExercise203()
        {
            SetupStubExercise203();

            // Test that response with malformed response body is sent when cookie is set
            HttpResponseMessage response = Given()
                .Spec(this.requestSpec)
                .Cookie(new Cookie("session", "invalid", "/requestLoan", "localhost"))
                .When()
                .Post("/requestLoan")
                .Then()
                .Extract()
                .Response();

            Assert.Throws<JsonReaderException>(() => JObject.Parse(response.Content.ToString()!));

            // Test that no response is sent when cookie is not set

            Given()
                .Spec(this.requestSpec)
                .When()
                .Post("/requestLoan")
                .Then()
                .StatusCode(HttpStatusCode.NotFound);
        }

        [Test]
        public void TestStubExercise204()
        {
            SetupStubExercise204();

            // Test that response is triggered when request body is present
            Given()
                .Spec(this.requestSpec)
                .ContentType("application/json")
                .Body(new { status = "active" })
                .When()
                .Post("/requestLoan")
                .Then()
                .StatusCode(HttpStatusCode.Created);

            // Test that response is not triggered when request body is not present
            Given()
                .Spec(this.requestSpec)
                .When()
                .Post("/requestLoan")
                .Then()
                .StatusCode(HttpStatusCode.NotFound);
        }
    }
}
