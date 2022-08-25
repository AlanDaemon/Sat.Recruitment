using Newtonsoft.Json;
using Sat.Recruitment.API.Models.Users;
using Sat.Recruitment.Domain.Enums;
using Sat.Recruitment.Domain.Features.Users.Entities;
using Sat.Recruitment.IntegrationTest;
using Sat.Recruitment.Test.IntegrationTests.Setup;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Sat.Recruitment.Test.IntegrationTests
{

    public class UserIntegrationTests : ScenarioBase, IClassFixture<ApplicationTestFixture>, IDisposable
    {

        private readonly ApplicationTestFixture fixture;
        private readonly List<User> usersToDispose;

        public UserIntegrationTests(ApplicationTestFixture fixture)
        {
            this.fixture = fixture;
            usersToDispose = new();
        }

        [Fact]
        public async void When_User_Not_Exists_Returns_Created_User()
        {
            var model = new AddUserCommandModel 
            {
                Name = "Bob",
                Address = "Fake Address 123",
                Email = "bob@gmail.com",
                Phone = "+4543242",
                Money = 101,
                UserType = UserTypes.Normal
            };
            var json = JsonConvert.SerializeObject(model);
            HttpContent param = new StringContent(json, Encoding.UTF8, "application/json");

            using var server = CreateServer();

            var response = await server.CreateClient().PostAsync("api/users/add?api-version=1", param);
            var result = response.Content.ReadAsStringAsync().Result;
            var user = JsonConvert.DeserializeObject<User>(result);
            usersToDispose.Add(user);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(user.Name, model.Name);
        }

        [Fact]
        public async void When_User_Exists_Does_Not_Interrupt()
        {
            var modelBob = new AddUserCommandModel
            {
                Name = "Bob",
                Address = "Fake Address 123",
                Email = "bob@gmail.com",
                Phone = "+4543242",
                Money = 101,
                UserType = UserTypes.Normal
            };

            var json = JsonConvert.SerializeObject(modelBob);
            HttpContent param = new StringContent(json, Encoding.UTF8, "application/json");

            using var server = CreateServer();

            var response = await server.CreateClient().PostAsync("api/users/add?api-version=1", param);
            var result = response.Content.ReadAsStringAsync().Result;
            var bob = JsonConvert.DeserializeObject<User>(result);

            // Alice has same email as Bob
            var modelAlice = new AddUserCommandModel
            {
                Name = "Alice",
                Address = "Other Address 123",
                Email = "bob@gmail.com",
                Phone = "+455667",
                Money = 44,
                UserType = UserTypes.SuperUser
            };

            json = JsonConvert.SerializeObject(modelAlice);
            param = new StringContent(json, Encoding.UTF8, "application/json");
            response = await server.CreateClient().PostAsync("api/users/add?api-version=1", param);
            result = response.Content.ReadAsStringAsync().Result;
            var alice = JsonConvert.DeserializeObject<User>(result);

            var modelJohn = new AddUserCommandModel
            {
                Name = "John",
                Address = "Address 123",
                Email = "john@gmail.com",
                Phone = "+645654",
                Money = 600,
                UserType = UserTypes.Normal
            };
            json = JsonConvert.SerializeObject(modelJohn);
            param = new StringContent(json, Encoding.UTF8, "application/json");

            response = await server.CreateClient().PostAsync("api/users/add?api-version=1", param);
            result = response.Content.ReadAsStringAsync().Result;
            var john = JsonConvert.DeserializeObject<User>(result);

            usersToDispose.Add(bob);
            usersToDispose.Add(alice);
            usersToDispose.Add(john);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(john.Name, modelJohn.Name);
        }

        public void Dispose()
        {
            fixture.context.Users.RemoveRange(usersToDispose);
            fixture.context.SaveChanges();
        }
    }
}
