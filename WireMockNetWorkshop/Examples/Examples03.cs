using NUnit.Framework;
using System;
using System.Net;
using System.Threading.Tasks;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace WireMockNetWorkshop.Examples
{
    [TestFixture]
    public class Examples03
    {
        private WireMockServer server;

        [SetUp]
        public void StartServer()
        {
            server = WireMockServer.Start(9876);
        }

        private void CreateStatefulStub()
        {
            server.Given(
                Request.Create().UsingGet().WithPath("/todo/items")
            )
            // In this scenario, when the current state is 'TodoList State Started',
            // a call to an HTTP GET will only return 'Buy milk'
           .InScenario("To do list")
           .WillSetStateTo("TodoList State Started")
           .RespondWith(
                Response.Create().WithBody("Buy milk")
           );

            server.Given(
                Request.Create().UsingPost().WithPath("/todo/items")
            )
            // In this scenario, when the current state is 'TodoList State Started',
            // a call to an HTTP POST will trigger a state transition to new state
            // 'Cancel newspaper item added'
            .InScenario("To do list")
            .WhenStateIs("TodoList State Started")
            .WillSetStateTo("Cancel newspaper item added")
            .RespondWith(
                Response.Create().WithStatusCode(201)
            );

            server.Given(
                Request.Create().UsingGet().WithPath("/todo/items")
            )
            // In this scenario, when the current state is 'Cancel newspaper item added',
            // a call to an HTTP GET will return 'Buy milk;Cancel newspaper subscription'
            .InScenario("To do list")
            .WhenStateIs("Cancel newspaper item added")
            .RespondWith(
                Response.Create().WithBody("Buy milk;Cancel newspaper subscription")
            );
        }

        [TearDown]
        public void StopServer()
        {
            server.Stop();
        }
    }
}
