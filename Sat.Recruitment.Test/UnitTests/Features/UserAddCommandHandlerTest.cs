using FakeItEasy;
using Sat.Recruitment.Application.Features.Users.Commands;
using Sat.Recruitment.Domain.Enums;
using Sat.Recruitment.Domain.Exceptions;
using Sat.Recruitment.Domain.Features.Users.Entities;
using Sat.Recruitment.Domain.Features.Users.Repository;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.UnitTests.Features.Users
{
    public class UserAddCommandHandlerTest
    {

        [Fact]
        public async Task Handle_UserAddedOK_ReturnUser()
        {
            //arrange          
            var user = new User()
            {
                Name = "Bob",
                Address = "Fake Address 123",
                Email = "bob@gmail.com",
                Phone = "+4543242",
                Money = 101,
                UserType = UserType.Normal
            };

            var command = new AddUserCommand()
            {
                User = user
            };
            var userRepository = A.Fake<IUserRepository>();
            var handler = new AddUserCommandHandler(userRepository);

            A.CallTo(() => userRepository.Add(command.User)).Returns(Task.FromResult(user));

            //act
            var result = await handler.Handle(command, CancellationToken.None);

            //assert
            Assert.Equal(user, result);
        }


        [Fact]
        public async Task Handle_InvalidMailFormat_ReturnsUserDomainException()
        {
            //arrange          
            var user = new User()
            {
                Name = "Bob",
                Address = "Fake Address 123",
                Email = "bobmail",
                Phone = "+4543242",
                Money = 101,
                UserType = UserType.Normal
            };

            var command = new AddUserCommand()
            {
                User = user
            };
            var userRepository = A.Fake<IUserRepository>();
            var handler = new AddUserCommandHandler(userRepository);

            //act
            var exception = await Assert.ThrowsAsync<DomainException>(() => handler.Handle(command, CancellationToken.None));

            //assert
            Assert.Equal("Error: invalid email format.", exception.Message);
        }      
    }
}
