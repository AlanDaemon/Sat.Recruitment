using Newtonsoft.Json;
using Sat.Recruitment.API.Models.Users;
using Sat.Recruitment.Domain.Enums;
using Sat.Recruitment.Domain.Features.Users.Entities;
using Sat.Recruitment.IntegrationTest;
using Sat.Recruitment.Test.IntegrationTests.Setup;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Sat.Recruitment.Test.IntegrationTests
{

    public class UserIntegrationTests : ScenarioBase, IClassFixture<ApplicationTestFixture>
    {

        readonly ApplicationTestFixture fixture;

        public UserIntegrationTests(ApplicationTestFixture fixture)
        {
            this.fixture = fixture;          
        }

        [Fact]
        public async void Given_non_existent_user_is_added_returns_user()
        {
            var model = new AddUserCommandModel 
            {
                Name = "Bob",
                Address = "Fake Address 123",
                Email = "bob@gmail.com",
                Phone = "+4543242",
                Money = 101,
                UserType = UserType.Normal
            };
            var json = JsonConvert.SerializeObject(model);
            HttpContent param = new StringContent(json, Encoding.UTF8, "application/json");

            using var server = this.CreateServer();

            var response = await server.CreateClient().PostAsync("api/users/add?api-version=1", param);
            var result = response.Content.ReadAsStringAsync().Result;
            var user = JsonConvert.DeserializeObject<User>(result);

            this.fixture.context.Users.Remove(user);
            this.fixture.context.SaveChanges();

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);           
        }

        [Fact]
        public async void Given_existent_user_is_added_returns_user()
        {
            var model = new AddUserCommandModel
            {
                Name = "Bob",
                Address = "Fake Address 123",
                Email = "bob@gmail.com",
                Phone = "+4543242",
                Money = 101,
                UserType = UserType.Normal
            };
            var json = JsonConvert.SerializeObject(model);
            HttpContent param = new StringContent(json, Encoding.UTF8, "application/json");

            using var server = this.CreateServer();

            var response = await server.CreateClient().PostAsync("api/users/add?api-version=1", param);
            var result = response.Content.ReadAsStringAsync().Result;
            var user = JsonConvert.DeserializeObject<User>(result);

            var model2 = new AddUserCommandModel
            {
                Name = "Alice",
                Address = "Other Address 123",
                Email = "bob@gmail.com",
                Phone = "+455667",
                Money = 44,
                UserType = UserType.SuperUser
            };           

            response = await server.CreateClient().PostAsync("api/users/add?api-version=1", param);
            result = response.Content.ReadAsStringAsync().Result;
            var user2 = JsonConvert.DeserializeObject<User>(result);

            this.fixture.context.Users.Remove(user);
            this.fixture.context.SaveChanges();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains($"User {user.Name} already exists in database", result);
        }
    }
}
