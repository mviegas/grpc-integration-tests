using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc.Testing;
using Service;
using Xunit;

namespace Tests
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _fixture;

        public UnitTest1(WebApplicationFactory<Startup> fixture)
        {
            _fixture = fixture;
        }
        
        [Fact]
        public void Test1()
        {
            using var httpClient = _fixture.CreateClient();

            var channel = GrpcChannel.ForAddress(httpClient.BaseAddress!, new GrpcChannelOptions()
            {
                HttpClient = httpClient
            });

            var greeterClient = new Greeter.GreeterClient(channel);

            var reply = greeterClient.SayHello(new HelloRequest());

            Assert.Contains("Hello", reply.Message);
        }
    }
}
