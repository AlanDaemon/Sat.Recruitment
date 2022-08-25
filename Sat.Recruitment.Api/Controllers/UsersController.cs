using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.API.Models.Users;
using Sat.Recruitment.Application.Features.Users.Commands;
using Sat.Recruitment.Domain.Enums;
using Sat.Recruitment.Domain.Features.Users.Entities;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public partial class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }     

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddUserCommandModel model)
        {
            var addUserCommand = new AddUserCommand()
            {
                User = new()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Address = model.Address,
                    Phone = model.Phone,
                    UserTypeId = UserTypes.Normal,
                    Money = model.Money
                }
            };
            return CreatedAtAction("add", await mediator.Send(addUserCommand));
        }

        [HttpPost("add-from-file")]
        public async Task<IActionResult> AddFromFile(string FileAbsolutePath)
        {
            var addUsersFromFileCommand = new AddUsersFromFileCommand()
            {
                FileAbsolutePath = FileAbsolutePath
            };
            return Ok(await mediator.Send(addUsersFromFileCommand));
        }
    }   
}
