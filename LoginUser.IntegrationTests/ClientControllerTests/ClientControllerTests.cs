using FluentAssertions;
using LoginUser.WebApi;
using LoginUser.WebApi.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LoginUser.IntegrationTests.ClientControllerTests
{
    public class ClientControllerTests : IClassFixture<WebApplicationFactory<Startup>>//wspoldzielenie factory testy nieco szybsze
    {
        private HttpClient _client;

        public ClientControllerTests(WebApplicationFactory<Startup> factory)
        {
             _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_Client_ReturnOk()
        {

            //act
            var response = await _client.GetAsync("/api/Client");

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_Client_ReturnOk()
        {
            //arrange
            var clientDto = new ClientDto()
            {
                LastName="Test",
                FirstName="Tescik",
                DataOfBirth=DateTime.Now,
                CreatedById = 1,
                Email = "malpa@example.com",
                Nationality = "usa"
            };

            var content = new StringContent(JsonConvert.SerializeObject(clientDto), UnicodeEncoding.UTF8, "application/json");

            //act
            var response = await _client.PostAsync("/api/Client/CreateWithoutAuthorize", content);

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        [Fact]
        public async Task Create_Client_ReturnInternalServerError()
        {
            //arrange
            var clientDto = new ClientDto()
            {
                Id = 1,//nie mozna uzywac w body id bo entity samo nadaje
                LastName = "Test",
                FirstName = "Tescik",
                DataOfBirth = DateTime.Now,
                CreatedById = 1,
                Email = "malpa@example.com",
                Nationality = "usa"
            };

            var content = new StringContent(JsonConvert.SerializeObject(clientDto), UnicodeEncoding.UTF8, "application/json");

            //act
            var response = await _client.PostAsync("/api/Client/CreateWithoutAuthorize", content);

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.InternalServerError);
        }
    }
}
