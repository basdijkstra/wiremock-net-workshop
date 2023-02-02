using NUnit.Framework;
using System;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace WireMockNetWorkshop.Examples
{
    [TestFixture]
    public class Examples02
    {
        private WireMockServer server;

        [SetUp]
        public void StartServer()
        {
            server = WireMockServer.Start(9876);
        }

        private void StubUrlMatching()
        {
            server.Given(
                Request.Create().UsingGet().WithPath("/url-matching")
            )
            .RespondWith(
                Response.Create()
                .WithBody("URL matching")
            );
        }

        private void StubHeaderMatching()
        {
            server.Given(
                Request.Create().UsingGet().WithPath("/header-matching")
                .WithHeader("header_name", new ExactMatcher("header_value"))
            )
            .RespondWith(
                Response.Create()
                .WithBody("Header matching")
            );
        }

        private void StubCookieMatching()
        {
            server.Given(
                Request.Create().UsingGet().WithPath("/cookie-matching")
                .WithCookie("cookie_name", new ExactMatcher("cookie_value"))
            )
            .RespondWith(
                Response.Create()
                .WithBody("Cookie matching")
            );
        }

        private void StubJsonBodyMatching()
        {
            server.Given(
                Request.Create().UsingGet().WithPath("/json-body-matching")
                .WithBody(new JmesPathMatcher("fruit == 'banana'"))
                .WithBody(new JmesPathMatcher("contains(date, '2023')"))
            )
            .RespondWith(
                Response.Create()
                .WithBody("JSON request body matching")
            );
        }

        private void StubReturnErrorStatusCode()
        {
            server.Given(
                Request.Create().UsingGet().WithPath("/error-status-code")
            )
            .RespondWith(
                Response.Create()
                .WithStatusCode(500)
            );
        }

        private void StubReturnResponseWithDelay()
        {
            server.Given(
                Request.Create().UsingGet().WithPath("/delayed-response")
            )
            .RespondWith(
                Response.Create()
                .WithStatusCode(200)
                .WithDelay(TimeSpan.FromMilliseconds(2000))
            );
        }

        private void StubReturnResponseWithFault()
        {
            server.Given(
                Request.Create().UsingGet().WithPath("/fault-response")
            )
            .RespondWith(
                Response.Create()
                .WithFault(FaultType.EMPTY_RESPONSE)
            );
        }

        [TearDown]
        public void StopServer()
        {
            server.Stop();
        }
    }
}
