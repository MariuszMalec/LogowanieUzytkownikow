using FluentAssertions;
using LoginUser.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;

namespace LoginUser.IntegrationTests.ClientControllerTests
{
    public class ClientControllerTests
    {
        [Fact]
        public async Task GetAll_Client_ReturnOk()
        {
            //arrange
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();

            //act
            var response = await client.GetAsync("/api/Client");

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
