using MediatR;
using Sat.Recruitment.Application.Features.Users.GiftCalculator;
using Sat.Recruitment.Domain.Exceptions;
using Sat.Recruitment.Domain.Features.Users.Entities;
using Sat.Recruitment.Domain.Features.Users.Repository;
using System.Linq;
using System.Text.RegularExpressions;
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
       
        public AddUserCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var validEmail = Regex.Match(request.User.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

            if (!validEmail.Success)
            {
                throw new DomainException("Error: invalid email format.");
            }

            var userExistsFilter = new UserExpressionFilters().Exists(request.User);
            var userExists = await this.userRepository.GetWhere(userExistsFilter);

            if (userExists.Any())
            {
                throw new DomainException($"User {request.User.Name} already exists in database");
            }            

            request.User.Money = GiftCalculatorFactory.GetCalculator(request.User.UserType).Calculate(request.User.Money);
            
            return await this.userRepository.Add(request.User);
        }
    }
}
