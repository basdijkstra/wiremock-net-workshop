using NUnit.Framework;
using System.Net;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

using static RestAssured.Dsl;

namespace WireMockNetWorkshop.Answers
{
    public class Answers01 : TestBase
    {
        private void SetupStubExercise101()
        {
            /************************************************
		     * Create a stub that will respond to a POST
		     * to /requestLoan with an HTTP status code 200
		     ************************************************/

            server.Given(
                Request.Create().UsingPost().WithPath("/requestLoan")
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
                Request.Create().UsingPost().WithPath("/requestLoan")
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
                Request.Create().UsingPost().WithPath("/requestLoan")
            )
            .RespondWith(
                Response.Create()
                .WithBody("Loan application received!")
            );
        }

        [Test]
        public void TestStubExercise101()
        {
            SetupStubExercise101();

            Given()
                .Spec(this.requestSpec)
                .When()
                .Post("/requestLoan")
                .Then()
                .StatusCode(HttpStatusCode.OK);
        }

        [Test]
        public void TestStubExercise102()
        {
            SetupStubExercise102();

            Given()
                .Spec(this.requestSpec)
                .When()
                .Post("/requestLoan")
                .Then()
                .ContentType("text/plain");
        }

        [Test]
        public void TestStubExercise103()
        {
            SetupStubExercise103();

            Given()
                .Spec(this.requestSpec)
                .When()
                .Post("/requestLoan")
                .Then()
                .Body("Loan application received!");
        }
    }
}
