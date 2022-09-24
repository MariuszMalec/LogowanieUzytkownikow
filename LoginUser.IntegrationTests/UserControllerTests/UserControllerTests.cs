using FluentAssertions;
using LoginUser.IntegrationTests.Fakes;
using LoginUser.WebApi;
using LoginUser.WebApi.Context;
using LoginUser.WebApi.Entities;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LoginUser.IntegrationTests.UserControllerTests
{
    public class UserControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private HttpClient _client;

        public UserControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var dbContextOptions = services
                            .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                        services.Remove(dbContextOptions);

                        services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();//oszukanie autoryzacji

                        services.AddMvc(option => option.Filters.Add(new FakeUserFilter()));//TODO dodajemy wlasny filtr aby nadpisac co nie dziala wyzej

                        services
                         .AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("UsersDb"));

                    });
                })
                .CreateClient();
        }

        //TODO uderzanie do endpointu z autoryzacja
        [Fact]
        public async Task PutUser_WithValidModel_ReturnsOk()
        {
            // arrange
            var model = new User()
            {   
                FirstName = "",
                LastName = "",
                Email = "",
                DataOfBirth = DateTime.Now,
                RoleId = 1,
                Nationality = "french",
                PasswordHash = ""
            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(model), UnicodeEncoding.UTF8, "application/json");

            // act
            var response = await _client.PutAsync("/api/users", httpContent);

            //TODO tu skonczylem https://youtu.be/6keSabBQRdE?t=4059

            // assert 

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAll_Users_ReturnOk()
        {

            //act
            var response = await _client.GetAsync("/api/Users");

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

    }
}
