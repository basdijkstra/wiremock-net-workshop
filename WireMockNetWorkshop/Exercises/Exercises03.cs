using NUnit.Framework;
using RestSharp;
using System.Net;
using System.Threading.Tasks;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

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
        public async Task TestStubExercise301()
        {
            SetupStubExercise301();

            // First request, no loan found
            RestRequest request = new RestRequest("/loan/12345", Method.Get);
            RestResponse response = await client.ExecuteAsync(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

            // Second request, submit the loan request
            request = new RestRequest("requestLoan", Method.Post);
            request.AddBody("Loan ID: 12345");
            response = await client.ExecuteAsync(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            // Third request, loan found
            request = new RestRequest("/loan/12345", Method.Get);
            response = await client.ExecuteAsync(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Content, Is.EqualTo("Loan ID: 12345"));
        }
    }
}