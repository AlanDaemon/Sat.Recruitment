using FakeItEasy;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Application.Features.Users.Commands;
using Sat.Recruitment.Application.Features.Users.Validations;
using Sat.Recruitment.Domain.Enums;
using Sat.Recruitment.Domain.Features.Users.Entities;
using Sat.Recruitment.Domain.Features.Users.Repository;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.UnitTests.Features.Users
{
    public class UserAddCommandHandlerTest
    {
        private AddUserCommandValidator validator  = new AddUserCommandValidator();

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
                UserTypeId = UserTypes.Normal
            };

            var command = new AddUserCommand()
            {
                User = user
            };
            var userRepository = A.Fake<IUserRepository>();
            var logger = A.Fake<ILogger<AddUserCommandHandler>>();
            var handler = new AddUserCommandHandler(userRepository, logger);

            A.CallTo(() => userRepository.Add(command.User)).Returns(Task.FromResult(user));

            //act
            var result = await handler.Handle(command, CancellationToken.None);

            //assert
            Assert.Equal(user, result);
        }


        [Fact]
        public void Handle_InvalidMailFormat_ReturnsEmailFormatError()
        {
            //arrange          
            var user = new User()
            {
                Name = "Bob",
                Address = "Fake Address 123",
                Email = "bobmail",
                Phone = "+4543242",
                Money = 101,
                UserTypeId = UserTypes.Normal
            };

            var command = new AddUserCommand()
            {
                User = user
            };

            //act
            var result = validator.Validate(command);

            //assert                        
            Assert.Equal("Error: invalid email format.", result.Errors.FirstOrDefault().ErrorMessage);
        }      
    }
}
