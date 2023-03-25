using NUnit.Framework;
using System.Net;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

using static RestAssured.Dsl;

namespace WireMockNetWorkshop.Exercises
{
    public class Exercises03 : TestBase
    {
        private void SetupStubExercise301()
        {
            /************************************************
		     * Create a stub that exerts the following behavior:
		     * - The scenario is called 'Loan processing'
		     * - 1. A first GET to /loan/12345 returns HTTP 404
		     * - 2. A POST to /requestLoan with body 'Loan ID: 12345'
		     *      returns HTTP 201 and causes a transition to
		     *      state 'LOAN_GRANTED'
		     * - 3. A second GET (when in state 'LOAN_GRANTED')
		     *     to /loan/12345 returns HTTP 200 and
		     *     body 'Loan ID: 12345'
		     ************************************************/

        }

        [Test]
        public void TestStubExercise301()
        {
            SetupStubExercise301();

            // First request, no loan found
            Given()
                .Spec(this.requestSpec)
                .When()
                .Get("/loan/12345")
                .Then()
                .StatusCode(HttpStatusCode.NotFound);

            // Second request, submit the loan request
            Given()
                .Spec(this.requestSpec)
                .Body("Loan ID: 12345")
                .When()
                .Post("/requestLoan")
                .Then()
                .StatusCode(HttpStatusCode.Created);

            // Third request, loan found
            Given()
                .Spec(this.requestSpec)
                .When()
                .Get("/loan/12345")
                .Then()
                .StatusCode(HttpStatusCode.OK)
                .Body("Loan ID: 12345");
        }
    }
}
