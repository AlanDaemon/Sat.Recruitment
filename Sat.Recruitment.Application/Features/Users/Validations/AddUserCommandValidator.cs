using FluentValidation;
using Sat.Recruitment.Application.Features.Users.Commands;
using System.Text.RegularExpressions;

namespace Sat.Recruitment.Application.Features.Users.Validations
{

    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(x => x.User.Email)
                   .NotEmpty()
                   .Matches(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase)
                   .WithMessage("Error: invalid email format.");            
        }     
    }

}
