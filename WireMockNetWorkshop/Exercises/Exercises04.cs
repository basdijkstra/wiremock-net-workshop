using NUnit.Framework;
using System.Net;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

using static RestAssured.Dsl;

namespace WireMockNetWorkshop.Exercises
{
    public class Exercises04 : TestBase
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

        }

        [Test]
        public void TestStubExercise401()
        {
            SetupStubExercise401();

            Given()
                .Spec(this.requestSpec)
                .When()
                .Get("/echo-port")
                .Then()
                .StatusCode(HttpStatusCode.OK)
                .Body("Listening on port 9876");
        }

        [TestCase(1000, TestName = "Loan application for $1000")]
        [TestCase(1500, TestName = "Loan application for $1500")]
        [TestCase(3000, TestName = "Loan application for $3000")]
        public void TestStubExercise402(int loanAmount)
        {
            SetupStubExercise402();

            var requestBody = new
            {
                loanDetails = new
                {
                    amount = loanAmount
                }
            };

            Given()
                .Spec(this.requestSpec)
                .ContentType("application/json")
                .Body(requestBody)
                .When()
                .Post("/echo-loan-amount")
                .Then()
                .StatusCode(HttpStatusCode.Created)
                .Body($"Received loan application request for ${loanAmount}");
        }
    }
}
