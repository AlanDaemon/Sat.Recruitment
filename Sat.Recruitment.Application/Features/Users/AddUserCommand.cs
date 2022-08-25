using MediatR;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Application.Features.Users.GiftCalculator;
using Sat.Recruitment.Domain.Enums;
using Sat.Recruitment.Domain.Features.Users.Entities;
using Sat.Recruitment.Domain.Features.Users.Repository;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Users.Domain.Features.Users;

namespace Sat.Recruitment.Application.Features.Users.Commands
{
    public class AddUserCommand : IRequest<User>
    {
        public User User { get; set; }
    }

    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, User>
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger<AddUserCommandHandler> logger;

        public AddUserCommandHandler(IUserRepository userRepository, ILogger<AddUserCommandHandler> logger)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var userExistsFilter = new UserExpressionFilters().Exists(request.User);
            var userExists = await userRepository.GetWhere(userExistsFilter);

            if (userExists.Any())
            {
                logger.LogInformation("User {request.User.Name} already exists in database", request.User.Name);
                return userExists.FirstOrDefault();
            }            

            request.User.Money = GiftCalculatorFactory.GetCalculator(request.User.UserTypeId).Calculate(request.User.Money);
            
            return await userRepository.Add(request.User);
        }
    }
}
