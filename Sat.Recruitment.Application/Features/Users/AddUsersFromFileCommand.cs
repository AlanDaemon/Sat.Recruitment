using MediatR;
using Sat.Recruitment.Domain.Enums;
using Sat.Recruitment.Domain.Features.Users.Entities;
using Sat.Recruitment.Domain.Features.Users.Repository;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Features.Users.Commands
{
    public class AddUsersFromFileCommand : IRequest<bool>
    {
        public string FileAbsolutePath;
    }

    public class AddUsersFromFileCommandHandler : IRequestHandler<AddUsersFromFileCommand, bool>
    {
        private readonly IUserRepository userRepository;
       
        public AddUsersFromFileCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> Handle(AddUsersFromFileCommand request, CancellationToken cancellationToken)
        {
            var addUserCommandHandler = new AddUserCommandHandler(this.userRepository);

            foreach (var line in File.ReadLines(@$"{request.FileAbsolutePath}"))
            {
                var addUserCommand = new AddUserCommand()
                {
                    User = this.MapFileLineToUser(line)
                };           
                
                await addUserCommandHandler.Handle(addUserCommand, cancellationToken);
            }        

            return true;
        }

        private User MapFileLineToUser(string line)
        {
            var userRawData = line.Split(',');

            return new User() {
                Name = userRawData[0],
                Email = userRawData[1],
                Phone = userRawData[2],
                Address = userRawData[3],
                UserType = Enum.Parse<UserType>(userRawData[4]),
                Money = decimal.Parse(userRawData[5])
            };
        }
    }
}
