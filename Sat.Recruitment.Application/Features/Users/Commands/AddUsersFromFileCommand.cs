using MediatR;
using Sat.Recruitment.Application.Features.Users.Files;
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
        private readonly IMediator mediator;
       
        public AddUsersFromFileCommandHandler(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> Handle(AddUsersFromFileCommand request, CancellationToken cancellationToken)
        {
            foreach (var line in File.ReadLines(@$"{request.FileAbsolutePath}"))
            {
                await mediator.Send(new AddUserCommand()
                {
                    User = UsersFileFormatter.MapFileLineToUser(line)
                }, cancellationToken);           
            }        

            return true;
        }      
    }
}
