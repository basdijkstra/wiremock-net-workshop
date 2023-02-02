using NUnit.Framework;
using RestSharp;
using System.Net;
using System.Threading.Tasks;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace WireMockNetWorkshop.Answers
{
    public class Answers04 : TestBase
    {
        private void SetupStubExercise401()
        {
            /************************************************
		     * Create a stub that responds to all GET requests
		     * to /echo-port with HTTP status code 200 and a
		     * response body containing the text
		     * "Listening on port <portnumber>", where <portnumber>
		     * is replaced with the actual port number
		     * (9876, in this case) using response templating.
		     * 
		     * The property to be used is called request.port
		     ************************************************/

            server.Given(
                Request.Create().UsingGet().WithPath("/echo-port")
            )
           .RespondWith(
                Response.Create()
                .WithStatusCode(200)
                .WithBody("Listening on port {{request.port}}")
                .WithTransformer()
           );
        }

        private void SetupStubExercise402()
        {
            /************************************************
		     * Create a stub that listens at path /echo-loan-amount
		     * and responds to all POST requests with HTTP
		     * status code 201 and a response body containing
		     * the text 'Received loan application request for $<amount>',
		     * where <amount> is the value of the JSON element
		     * loanDetails.amount extracted from the request body
		     ************************************************/

            server.Given(
                Request.Create().UsingPost().WithPath("/echo-loan-amount")
            )
           .RespondWith(
                Response.Create()
                .WithStatusCode(201)
                .WithBody("Received loan application request for ${{JsonPath.SelectToken request.body \"$.loanDetails.amount\"}}")
                .WithTransformer()
           );
        }

        [Test]
        public async Task TestStubExercise401()
        {
            SetupStubExercise401();

            RestRequest request = new RestRequest("/echo-port", Method.Get);

            RestResponse response = await client.ExecuteAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Content, Is.EqualTo("Listening on port 9876"));
        }

        [TestCase(1000, TestName = "Loan application for $1000")]
        [TestCase(1500, TestName = "Loan application for $1500")]
        [TestCase(3000, TestName = "Loan application for $3000")]
        public async Task TestStubExercise402(int loanAmount)
        {
            SetupStubExercise402();

            var requestBody = new
            {
                loanDetails = new
                {
                    amount = loanAmount
                }
            };

            RestRequest request = new RestRequest("/echo-loan-amount", Method.Post);

            request.AddJsonBody(requestBody);

            RestResponse response = await client.ExecuteAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(response.Content, Is.EqualTo($"Received loan application request for ${loanAmount}"));
        }
    }
}
